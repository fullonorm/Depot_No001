using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Connect : MonoBehaviourPunCallbacks
{
    GameManager gameManager;

    //private string LogoutUrl = "https://192.168.0.161:8443/Logout.php";
    private string LogoutUrl = "https://114.204.129.206:9001/Logout.php";
    bool quit = false;

    private void Awake()
    {
        gameManager = GameManager.GetInstance();

    }


    public override void OnConnectedToMaster()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        StartCoroutine(Logout());
        print("Disconnted from server for return" + cause.ToString());
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            quit = true;
            StartCoroutine(Logout());
        }
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

}