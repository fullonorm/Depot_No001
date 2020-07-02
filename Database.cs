using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Database : MonoBehaviour
{
    // 싱글톤
    private Database() { }
    private static Database instance = null;
    public static Database GetInstance()
    {
        if (instance == null)
        {
            var obj = FindObjectOfType<Database>();
            if (obj != null)
            {
                instance = obj;
            }
            else
            {
                var newDatabase = new GameObject("Database").AddComponent<Database>();
                instance = newDatabase;
            }
        }
        return instance;
    }

    private void Awake()
    {
        gameManager = GameManager.GetInstance();

        var objs = FindObjectsOfType<Database>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        
    }

    public GameManager gameManager;
    private ItemBox itemBox;


    private string Url = "https://114.204.129.206:9001/";
    private string ReadItemUrl;
    private string WriteItemUrl;
    private string ScoreUrl;

    private void Start()
    {
        ReadItemUrl = Url + "LoadItem.php";
        WriteItemUrl = Url + "WriteItem.php";
        ScoreUrl = Url + "Score.php";

        gameManager = GameManager.GetInstance();
        itemBox = gameManager.itemBox;


    }

    public void RewardUpdate(int score)
    {
        score /= 10;
        itemBox.ModifyItem(201, score);
        itemBox.ModifyItem(202, score);
        itemBox.ModifyItem(203, score);
        itemBox.ModifyItem(204, score);
        itemBox.ModifyItem(205, score);
        ItemDataWrite();
    }


    public void ItemDataRead()
    {
        Debug.Log("아이템 읽기");
        StartCoroutine(ReadItem());
    }

    public void ItemDataWrite()
    {
        string itemString = itemBox.GetBoxString();
        StartCoroutine(WriteItem(itemString));
    }

    public void SetItem(string ItemString)
    {
        Debug.Log(ItemString);
        string[] itemArray = ItemString.Split('/');

        Dictionary<int, int> box = new Dictionary<int, int>();

        foreach (string i in itemArray)
        {
            string[] item = i.Split(',');
            box.Add(int.Parse(item[0]), int.Parse(item[1]));
        }

        itemBox.SetBox(box);
    }
     

    IEnumerator ReadItem()
    {
        WWWForm form = new WWWForm();
        UnityWebRequest request = UnityWebRequest.Post(ReadItemUrl, form);
        {
            request.certificateHandler = new CertPublicKey { PUB_KEY = gameManager.pubkey };
            yield return request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);
            Debug.Log("아이템 값 읽기");
            string result = request.downloadHandler.text;              
            SetItem(request.downloadHandler.text);
        }
    }

    IEnumerator WriteItem(string itemString)
    {
        WWWForm form = new WWWForm();
        form.AddField("WInput", itemString);
        UnityWebRequest request = UnityWebRequest.Post(WriteItemUrl, form);
        {
            request.certificateHandler = new CertPublicKey { PUB_KEY = gameManager.pubkey };
            yield return request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);
            ItemDataRead();
        }
    }

    public void SaveScore(int score)
    {
        StartCoroutine(SaveSc(score));
    }

    IEnumerator SaveSc(int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("Score", score);
        UnityWebRequest request = UnityWebRequest.Post(ScoreUrl, form);
        {
            request.certificateHandler = new CertPublicKey { PUB_KEY = gameManager.pubkey };
            yield return request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);
        }
    }

}
