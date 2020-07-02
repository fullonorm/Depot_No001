using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FHK_Bullet : MonoBehaviour
{
    //public bool IsActive = true;

    public float Bullet_DMG = 4;
    public float Bullet_RNG = 2;

    private float bulletSpeed = 1000.0f;
    private Transform thisTransform;

    void Start()
    {
        thisTransform = GetComponent<Transform>();

        FireBullet();
        Destroy(gameObject, Bullet_RNG);
    }

    void FireBullet()
    {
        GetComponent<Rigidbody>().AddForce(thisTransform.forward * bulletSpeed);
    }

    // 손댈
    // 총격
    //[PunRPC]
    public void FHKfunc_BulletAttack(Collider other)
    {
        other.GetComponent<FHK_Enemy_Status>().Enemy_HP
                -= Bullet_DMG * (100 - other.GetComponent<FHK_Enemy_Status>().Enemy_DEF) * 0.01f;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            JYH_PlayEffect eff = GetComponent<JYH_PlayEffect>();
            eff.tf = this.transform;
            eff.playPraticle = true;

            // 손댈
            // 피격된 적 HP 공유
            FHKfunc_BulletAttack(other);
            //photonView.RPC("FHKfunc_BulletAttack", RpcTarget.All, other);
        }
    }
}
