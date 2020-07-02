using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FHK_Enemy_Chasing : MonoBehaviourPun
{
    public bool IsEnemyKeepChasing;
    private bool IsPlayerInRange;

    private float e_spd;

    private float e_dmg;
    private float e_rng;
    private float e_rof;
    private float rofFloat;
    
    private GameObject vehicle;

    void Start()
    {
        IsEnemyKeepChasing = true;
        IsPlayerInRange = false;

        FHK_Enemy_Status es = gameObject.GetComponent<FHK_Enemy_Status>();

        e_spd = es.Enemy_SPD;
        e_dmg = es.Enemy_DMG;
        e_rng= es.Enemy_RNG;
        e_rof = es.Enemy_ROF;

        vehicle = GameObject.Find("Vehicle");
    }

    // 손댈 - 적 공격과 체력 조정
    [PunRPC]
    public void FHKfunc_EnemyAttack()
    {
        vehicle.GetComponent<FHK_Vehicle_Status>().Vehicle_HP
                            -= e_dmg * (100 - vehicle.GetComponent<FHK_Vehicle_Status>().Vehicle_DEF) * 0.01f;
    }

    void Update()
    {
        // IsEnemyKeepChasing이 true일 때만 추적기능
        if (IsEnemyKeepChasing)
        {
            gameObject.transform.LookAt(vehicle.transform);
            gameObject.transform.Translate(Vector3.forward * e_spd);

            if (FHK_StageManager.IsStageOngoing)
            {
                if (e_rng > (vehicle.transform.position - gameObject.transform.position).magnitude)
                {
                    IsPlayerInRange = true;
                }
                else
                {
                    IsPlayerInRange = false;
                }

                // IsPlayerInRange가 true일 때만 타이머에 따라 공격
                if (IsPlayerInRange)
                {
                    rofFloat += Time.deltaTime * e_rof;
                    if (rofFloat >= 1)
                    {

                        rofFloat = 0;

                        // 손댈
                        // 깎이는 HP 공유해줘야함
                        // 공격력 * (100-방어력)/100 = 피격데미지
                        // FHKfunc_EnemyAttack();
                        photonView.RPC("FHKfunc_EnemyAttack", RpcTarget.All);
                    }
                }
                else rofFloat = 0;
            }
        }
    }
}
