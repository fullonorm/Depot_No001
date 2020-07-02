using System.Security.Cryptography.X509Certificates;
using System.Text;
using UnityEngine;
using Photon.Pun;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // 싱글톤
    private GameManager() { }
    private static GameManager instance = null;
    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            var obj = FindObjectOfType<GameManager>();
            if (obj != null)
            {
                instance = obj;
            }
            else
            {
                var newGameManager = new GameObject("GameManager").AddComponent<GameManager>();
                instance = newGameManager;
            }
        }
        return instance;
    }

    private void CreateSingletonObject()
    {
        var objs = FindObjectsOfType<GameManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {
        CreateSingletonObject();
    }

    public Equipment equipment = new Equipment();
    public ItemInformation itemInformation = new ItemInformation();
    public ItemBox itemBox = new ItemBox();
    public Database database;
    public string pubkey;

    public bool isReturnGame = false;

    private void Start()
    {
        ReadCert();
        itemInformation.initLists();
        database = Database.GetInstance();
        //database.ItemDataRead();

        //itemBox.GetBoxString();
        
        // 기본적으로 처음에 장착하고 있는 장비/차량 
        equipment.SetWeapon(1);
        equipment.SetVehicle(101);
    }

    public void ReadCert()
    {
        //https인증에 필요한 PublicKey 생성
        TextAsset tx = Resources.Load("key") as TextAsset;
        byte[] by = Encoding.UTF8.GetBytes(tx.ToString());
        X509Certificate2 certificate = new X509Certificate2(by);
        pubkey = certificate.GetPublicKeyString();
    }
    
    public void ReturnGameRoom()
    {
        PhotonNetwork.JoinLobby();
        GameObject uiController = GameObject.Find("UIController");
        uiController.GetComponent<UIController>().returnGameRoom();
    }
    
    void Update()
    {
        if (isReturnGame)
        {
            isReturnGame = false;
            Invoke("ReturnGameRoom", 0.3f);
        }  
    }

    public int rndRoute;
    public bool IsSquadLeader; // 방장인가?
    // 각보직 참여 유무
    public bool IsDriverHere;
    public bool IsStriker01Here;
    public bool IsStriker02Here;
    public bool IsStriker03Here;

    public int AID; // 지역정보
    public int VID; // 차량정보
    public int PID; // driver : 0, striker : 1~3
    public int WID; // 플레이어의 캐릭터 모델링, 사수라면 총기정보까지 들어갈 자리    

}
