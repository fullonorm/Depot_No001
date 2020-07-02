using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class FHK_StageManager : MonoBehaviourPun
{
    [Header("GameData")]
    private float score;

    public int AID;
    public bool IsDriverHere;
    GameObject tempPlayerPrefab;

    [Header("Game System")]
    public int readyTime = 10;
    public static string systemMsg;
    public static bool IsStageStart;
    public static bool IsStageOngoing;
    public static bool IsStageEnd;
    public static bool IsStageCleared;
    public static float FirstTimer;
    public static float ElapsedTimer;

    [Header("SpawnTransform")]
    public Transform vehicleSpawnTf;
    public Transform driverSpawnTf;
    public Transform strikerSpawnTf01;
    public Transform strikerSpawnTf02;
    public Transform strikerSpawnTf03;

    [Header("Obstacle")]
    private int rndRoute;
    public GameObject Obstacle01;
    public GameObject Obstacle02;


    private GameObject VRSettings;

    public static FHK_StageManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<FHK_StageManager>();
            }
            return m_instance;
        }
    }

    private static FHK_StageManager m_instance;


    private void Awake()
    {
        if(instance != this)
        {
            Destroy(gameObject);
        }

        GameManager gm = GameManager.GetInstance();
        IsDriverHere = gm.IsDriverHere;
        AID = gm.AID;
        rndRoute = gm.rndRoute;
    }
    
    private void Start()
    {
        FirstTimer = 0;
        ElapsedTimer = 0;
        IsStageStart = true;

        FHKfunc_DestoyObstacle();

        VRSettings = GameObject.Find("VRSettings");
    }
    
    void FHKfunc_DestoyObstacle()
    {
        GetComponent<FHK_Auto_Driver_Area01>().routeNumber = rndRoute;
        // 장애물 랜덤 제거
        if (rndRoute > 0)
        {
            Destroy(Obstacle02);
        }
        else
        {
            Destroy(Obstacle01);
        }
    }

    void FHKfunc_EndMsg()
    {
        systemMsg = "곧 탐사를 종료합니다.";
    }
    void FHKfunc_returnToLobby()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_ReturnGameRoom", RpcTarget.All);
            PhotonNetwork.LoadLevel("Lobby");
        }
    }
    
    [PunRPC]
    private void RPC_ReturnGameRoom()
    {
        VRSettings.GetComponent<VRSettings>().VRSettingOff();
        GameManager.GetInstance().isReturnGame = true;
        Database database = Database.GetInstance();
        database.RewardUpdate((int)score);
        database.SaveScore((int)score);
    }


    private void Update()
    {

        if (IsStageStart)
        {
            FirstTimer += Time.deltaTime;
            systemMsg = "곧 시작합니다.";
            if (FirstTimer > readyTime)
            {
                systemMsg = "";
                IsStageStart = false;
                IsStageOngoing = true;
                Debug.Log("StageStart!");
                
                
            }
        }
        if (IsStageEnd)
        {
            IsStageOngoing = false;
            IsStageEnd = false;

            // 만약 클리어했다면?
            if (IsStageCleared)
            {
                // 보상계수 = 남은체력비율*100
                score = GameObject.Find("Vehicle").GetComponent<FHK_Vehicle_Status>().Vehicle_HP_Rate * 100;
                Debug.Log("score = "+score);
                systemMsg = "내구도 "+ (int)score +"% 보존";
            }
            else
            {
                systemMsg = "탐사에 실패했습니다.";
            }

            Invoke("FHKfunc_EndMsg", 3);


            // 손댈
            // 로비화면으로 복귀, 스코어 보내야됨
            // -> GameRoom 오브젝트 활성화 

            Invoke("FHKfunc_returnToLobby", 5);

        }

    }

}
