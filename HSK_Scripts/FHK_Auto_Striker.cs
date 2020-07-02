using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FHK_Auto_Striker : MonoBehaviour
{
    private Animator animator;
    public GameObject modelingPrefab;
    public GameObject Radar;

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

    public bool IsInRange;
    public bool IsFocusingOn;

    // Start is called before the first frame update
    void Start()
    {
        IsFocusingOn = false;

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
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            IsInRange = true;
        }
    }

    void Update()
    {
        if (FHK_StageManager.IsStageOngoing)
        {
            if (IsInRange)
            {
                IsInRange = false;
                Radar.SetActive(true);
                Radar.transform.localScale += (Vector3.forward + Vector3.right)* 6f *Time.deltaTime;
            }

            if (IsFocusingOn)
            {
                rofFloat += Time.deltaTime * w_rof;
                if (rofFloat >= 1)
                {
                    IsFocusingOn = false;
                    animator.SetTrigger("Shoot");
                    rofFloat = 0;
                    GameObject b = Instantiate(bullet, FirePos.transform);
                    b.GetComponent<FHK_Bullet>().Bullet_DMG = w_dmg;
                    b.GetComponent<FHK_Bullet>().Bullet_RNG = w_rng;
                }
            }
            else rofFloat = 0;
        }
    }
}
