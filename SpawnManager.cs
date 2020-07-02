using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviourPun
{

    GameObject tempPlayerPrefab;

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


    [Header("Vehicle Prefab")]
    public GameObject vehicle_DirtRed;
    public GameObject vehicle_DirtWhite;
    public GameObject vehicle_MetalPurple;
    [Header("Character&Weapon Prefab")]
    public GameObject striker_Auto;
    public GameObject driver_Basci;
    public GameObject striker_Hatter;
    public GameObject striker_Narval;
    public GameObject striker_Gasman;

    public bool IsPlayerSpawn;
    public bool IsVehicleSpawn;


    private FHK_StageManager stageManager;

    private void Awake()
    {
        stageManager = FHK_StageManager.instance;
        Debug.Log(stageManager.readyTime);
        setParam();
    }

    public void setParam()
    {
        // 손댈
        // 방장인가???
        // IsSquadLeader = true;

        GameManager gm = GameManager.GetInstance();
        IsSquadLeader = gm.IsSquadLeader;
        IsDriverHere = gm.IsDriverHere;
        IsStriker01Here = gm.IsStriker01Here;
        IsStriker02Here = gm.IsStriker02Here;
        IsStriker03Here = gm.IsStriker03Here;

        AID = gm.AID;
        VID = gm.VID;
        PID = gm.PID;
        WID = gm.WID;

    }

    [PunRPC]
    public void FHKfunc_PlayerReLocation(GameObject prefab, Transform spawnTf)
    {
        prefab.transform.SetParent(spawnTf);
        prefab.transform.localScale = Vector3.one;
    }

    private void Start()
    {
        // 운전수가 없으면 자동으로 흰색트럭으로
        if (!IsDriverHere)
        {
            VID = 102;
        }

        if (IsVehicleSpawn) FHKfunc_VehicleSpawn(VID);
        if (IsPlayerSpawn) FHKfunc_PlayerSpawn(WID, PID);

        if (IsSquadLeader)
        {
            // 손댈
            // 방장만 혼자 새로 생성해주기 때문에 이부분은 동기화 시켜줘야함
            // 보직이 비어있다면?
            if (!IsStriker01Here)
            {
                GameObject striker01 = PhotonNetwork.Instantiate(striker_Auto.name, stageManager.strikerSpawnTf01.position, stageManager.strikerSpawnTf01.rotation) as GameObject;
                striker01.transform.SetParent(stageManager.strikerSpawnTf01);
            }
            if (!IsStriker02Here)
            {
                GameObject striker02 = PhotonNetwork.Instantiate(striker_Auto.name, stageManager.strikerSpawnTf02.position, stageManager.strikerSpawnTf02.rotation) as GameObject;
                striker02.transform.SetParent(stageManager.strikerSpawnTf02);
                
            }
            if (!IsStriker03Here)
            {
                GameObject striker03 = PhotonNetwork.Instantiate(striker_Auto.name, stageManager.strikerSpawnTf03.position, stageManager.strikerSpawnTf03.rotation) as GameObject;
                striker03.transform.SetParent(stageManager.strikerSpawnTf03);
                
            }
        }


        void FHKfunc_VehicleSpawn(int VID)
        {
            switch (VID)
            {
                case 101: Instantiate(vehicle_DirtRed, stageManager.vehicleSpawnTf); break;
                case 102: Instantiate(vehicle_DirtWhite, stageManager.vehicleSpawnTf); break;
                case 103: Instantiate(vehicle_MetalPurple, stageManager.vehicleSpawnTf); break;
            }
        }

        void FHKfunc_PlayerSpawn(int WID, int PID)
        {
            // WeaponID(무기)에 따라 맞는 무기 및 캐릭터 모델링 결정
            switch (WID)
            {
                case 0: tempPlayerPrefab = driver_Basci; break;
                case 1: tempPlayerPrefab = striker_Hatter; break;
                case 2: tempPlayerPrefab = striker_Narval; break;
                case 3: tempPlayerPrefab = striker_Gasman; break;
            }

            // PositionID(보직)에 따라 스폰위치 결정
            switch (PID)
            {
                // driver는 없어도 autoMode있어서 굳이 인스티에이트 필요없음
                case 0:
                    GameObject driver = PhotonNetwork.Instantiate(tempPlayerPrefab.name, stageManager.driverSpawnTf.position, stageManager.driverSpawnTf.rotation) as GameObject;
                    driver.transform.SetParent(stageManager.driverSpawnTf);
                    
                    break;

                case 1:
                    GameObject striker01 = PhotonNetwork.Instantiate(tempPlayerPrefab.name, stageManager.strikerSpawnTf01.position, stageManager.strikerSpawnTf01.rotation) as GameObject;
                    striker01.transform.SetParent(stageManager.strikerSpawnTf01);
                    
                    break;

                case 2:
                    GameObject striker02 = PhotonNetwork.Instantiate(tempPlayerPrefab.name, stageManager.strikerSpawnTf02.position, stageManager.strikerSpawnTf02.rotation) as GameObject;
                    striker02.transform.SetParent(stageManager.strikerSpawnTf02);
                    
                    break;

                case 3:
                    GameObject striker03 = PhotonNetwork.Instantiate(tempPlayerPrefab.name, stageManager.strikerSpawnTf03.position, stageManager.strikerSpawnTf03.rotation) as GameObject;
                    striker03.transform.SetParent(stageManager.strikerSpawnTf03);
                    
                    break;
            }
        }

    }
}
