using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FHK_EnemySpawning : MonoBehaviourPun
{
    private int AID;
    public Transform EnemySpawnTf;
    
    private float timerFloat;

    private int ranX;
    private int ranZ;

    [Header("Enemy")]
    public float enemySpawnTime = 5;
    public GameObject turnipa_sweet;
    public GameObject turnipa_sour;

    void Start()
    {
        timerFloat = 0;
        AID = GetComponent<FHK_StageManager>().AID;
    }

    void Update()
    {
        if (FHK_StageManager.IsStageOngoing)
        {
            switch (AID)
            {
                case 0:
                    timerFloat += Time.deltaTime;
                    if (timerFloat >= enemySpawnTime)
                    {
                        timerFloat = 0;

                        if (Random.Range(0, 2) == 0) ranX = -1;
                        else ranX = 1;
                        if (Random.Range(0, 2) == 0) ranZ= -1;
                        else ranZ = 1;

                        if(Random.Range(0,4) <= 0)
                        {
                            PhotonNetwork.Instantiate(turnipa_sour.name, EnemySpawnTf.position + new Vector3(ranX * Random.Range(10, 20), 0, ranZ * Random.Range(10, 20)), EnemySpawnTf.rotation);
                        }
                        else
                        {
                            PhotonNetwork.Instantiate(turnipa_sweet.name, EnemySpawnTf.position + new Vector3(ranX * Random.Range(10, 20), 0, ranZ * Random.Range(10, 20)), EnemySpawnTf.rotation);
                        }
                        

                    }
                    break;
            }
        }
    }
}
