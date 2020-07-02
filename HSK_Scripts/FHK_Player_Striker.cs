using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FHK_Player_Striker : MonoBehaviourPun
{
    private Animator animator;
    public GameObject modelingPrefab;
    public GameObject privateCamera;

    // 탄 종류 결정은 WID를 입력받음
    private int wid;
    // 탄에게 전달할 공격력, 사거리(유효시간)
    private float w_dmg;
    private float w_rng;
    // 탄 발사 주기(발사속도)
    private float w_rof;
    private float rofFloat;
    // 탄창당 발수
    private float w_amo;
    // 재장전 시간
    private float w_rld;

    // 탄 생성 위치 지정
    public GameObject FirePos;

    // 탄 프리펩
    public GameObject bullet;

    private bool readyToFire;

    public bool IsPlayerFocusingOn;

    // Start is called before the first frame update
    void Start()
    {
        IsPlayerFocusingOn = false;

        rofFloat = 0;

        FHK_Weapon_Status ws = gameObject.GetComponent<FHK_Weapon_Status>();

        wid = ws.WID;
        w_dmg = ws.Weapon_DMG;
        w_rng = ws.Weapon_RNG;
        w_rof = ws.Weapon_ROF;
        w_amo = ws.Weapon_AMO;
        w_rld = ws.Weapon_RLD;

        animator = modelingPrefab.GetComponent<Animator>();
        animator.SetFloat("speed", w_rof);

        if (!photonView.IsMine)
        {
            privateCamera.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;
        if (FHK_StageManager.IsStageOngoing)
        {
            if (IsPlayerFocusingOn)
            {
                IsPlayerFocusingOn = false;
                rofFloat += Time.deltaTime * w_rof;
                if (rofFloat >= 1)
                {
                    animator.SetTrigger("Shoot");
                    rofFloat = 0;
                    GameObject b = PhotonNetwork.Instantiate(bullet.name, FirePos.transform.position, FirePos.transform.rotation);
                    b.GetComponent<FHK_Bullet>().Bullet_DMG = w_dmg;
                    b.GetComponent<FHK_Bullet>().Bullet_RNG = w_rng;
                }
            }
            else rofFloat = 0;
        }
    }
}
