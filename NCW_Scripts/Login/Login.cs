using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using Photon.Pun;
using Photon.Realtime;


public class Login : MonoBehaviourPunCallbacks
{

    [Header("LoginPanel")]
    public GameObject LoginPanel;
    public InputField IDInputField;
    public InputField PassInputField;

    [Header("CreateAccountPanel")]
    public GameObject CreateAccountPanel;
    public InputField New_IDInputField;
    public InputField New_PassInputField;
    public InputField New_RePassInputField;

    [Header("NickNamePanel")]
    public GameObject CreateNickNamePanel;
    public InputField NickName_InputField;

    public Button LoginBtn;

    private string LoginUrl;
    private string CreateUrl;
    private string NickUrl;
    private string ReLoginUrl;

    private string LoadNickUrl;
    private string SaveNickUrl;

    private string LoginResult;
    private string CreateResult;
    private string CreateNickName;

    GameManager gameManager;
    Database database;

    private string pubkey;

    //private string url = "https://192.168.0.161:8443/";
    private string url = "https://114.204.129.206:9001/";

    private string LogoutUrl;

    private void Awake()
    {
        gameManager = GameManager.GetInstance();
        database = Database.GetInstance();
    }

    private void Start()
    {
        LoginUrl = url + "Login.php";
        CreateUrl = url + "Enroll.php";
        NickUrl = url + "Nick.php";
        LogoutUrl = url + "Logout.php";
        SaveNickUrl = url + "SaveNick.php";

        PhotonNetwork.SendRate = 40;
        PhotonNetwork.SerializationRate = 15;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "0.0.0";
        PhotonNetwork.ConnectUsingSettings();

    }

    private void Update()
    {
        LoadNickUrl = url + "LoadNick.php";
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to Server!!");
        LoginBtn.interactable = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        StartCoroutine(Logout());
        print("Disconnted from server for return" + cause.ToString());
    }

    IEnumerator Logout()
    {
        WWWForm form = new WWWForm();
        UnityWebRequest webRequest = UnityWebRequest.Post(LogoutUrl, form);
        {
            webRequest.certificateHandler = new CertPublicKey { PUB_KEY = gameManager.pubkey };

            yield return webRequest.SendWebRequest();
        }
    }

    public void LoadLevel()
    {
        gameManager.database.ItemDataRead();
        Debug.Log("database 객체의 ItemDataRead() 실행");
        PhotonNetwork.LoadLevel("Lobby");
    }

    public void LoginButton()
    {
        StartCoroutine(LoginCo());
    }

    IEnumerator LoginCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("EMAIL", IDInputField.text);
        form.AddField("PASSWORD", PassInputField.text);

        UnityWebRequest request = UnityWebRequest.Post(LoginUrl, form);
        {
            request.certificateHandler = new CertPublicKey { PUB_KEY = gameManager.pubkey };
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log("Error While Sending: " + request.error);
            }
            else
            {
                Debug.Log("Received: " + request.downloadHandler.text);
                LoginResult = request.downloadHandler.text;
            }

            switch (LoginResult)
            {
                case "Login":
                    LoginPanel.SetActive(false);
                    CreateAccountPanel.SetActive(false);
                    CreateNickNamePanel.SetActive(true);
                    break;
                case "Exist":
                    StartCoroutine(LoadNick());
                    LoadLevel();
                    break;
                case "diff":
                    AlertViewController.Show("경고", "중복 로그인 입니다.", null);
                    break;

            }
        }
    }

    IEnumerator LoadNick()
    {
        WWWForm form = new WWWForm();
        UnityWebRequest webRequest = UnityWebRequest.Post(LoadNickUrl, form);
        {
            webRequest.certificateHandler = new CertPublicKey { PUB_KEY = gameManager.pubkey };

            yield return webRequest.SendWebRequest();

            PhotonNetwork.NickName = webRequest.downloadHandler.text;
            Debug.Log(webRequest.downloadHandler.text);
        }
    }

    public void OpenCreateAccountBtn()
    {
        LoginPanel.SetActive(false);
        CreateAccountPanel.SetActive(true);
        CreateNickNamePanel.SetActive(false);
    }

    public void closeCreateAccountBtn()
    {
        LoginPanel.SetActive(true);
        CreateAccountPanel.SetActive(false);
        CreateNickNamePanel.SetActive(false);
    }

    public void CreateAccountBtn()
    {
        StartCoroutine(CreateCo());
    }

    IEnumerator CreateCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("EMAIL", New_IDInputField.text);
        form.AddField("PASSWORD", New_PassInputField.text);
        form.AddField("REPASSWORD", New_RePassInputField.text);
        UnityWebRequest webRequest = UnityWebRequest.Post(CreateUrl, form);
        {
            webRequest.certificateHandler = new CertPublicKey { PUB_KEY = gameManager.pubkey };
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error While Sending: " + webRequest.error);
            }
            else
            {
                CreateResult = webRequest.downloadHandler.text;
                Debug.Log("Received: " + CreateResult);
            }
            switch (CreateResult)
            {
                case "empty":
                    AlertViewController.Show("경고", "입력하세요.", null);
                    break;
                case "Exist ID":
                    AlertViewController.Show("경고", "존재하는 ID 입니다.", null);
                    break;
                case "pwd":
                    AlertViewController.Show("경고", "비밀번호가 일치하지 않습니다.", null);
                    break;
                case "Enroll\n":
                    LoginPanel.SetActive(true);
                    CreateAccountPanel.SetActive(false);
                    CreateNickNamePanel.SetActive(false);
                    break;
            }
        }
    }

    public void CreateNicknameBtn()
    {
        StartCoroutine(CreateNick());
    }

    IEnumerator CreateNick()
    {
        WWWForm form = new WWWForm();
        form.AddField("NICKNAME", NickName_InputField.text);
        UnityWebRequest webRequest = UnityWebRequest.Post(NickUrl, form);
        {
            webRequest.certificateHandler = new CertPublicKey { PUB_KEY = gameManager.pubkey };
            yield return webRequest.SendWebRequest();

            Debug.Log(webRequest.downloadHandler.text);
            CreateNickName = webRequest.downloadHandler.text;

            switch (CreateNickName)
            {
                case "Success":
                    StartCoroutine(LoadNick());
                    LoadLevel();
                    break;
                case "Exist":
                    AlertViewController.Show("경고", "중복 닉네임 입니다.", null);
                    break;
                case "Fail":
                    AlertViewController.Show("경고", "닉네임을 입력하세요.", null);
                    break;
            }
        }
    }
}