using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FHK_Enemy_Status : MonoBehaviour
{
    public int EnemyState = 0;

    public float Enemy_HP = 10.0f;
    private float Enemy_prevHP;
    private float Enemy_originHP;

    public float Enemy_DEF;

    public float Enemy_DMG;

    public float Enemy_RNG;

    public float Enemy_ROF;
    
    public float Enemy_SPD;

    public GameObject HP_Bar;

    private void Start()
    {
        Enemy_originHP = Enemy_HP;
        Enemy_prevHP = Enemy_HP;
        Destroy(gameObject,30);
    }

    private void Update()
    {
        if (Enemy_prevHP > Enemy_HP)
        {
            Enemy_prevHP -= Time.deltaTime * Enemy_originHP * 3;
            if (Enemy_prevHP < Enemy_HP) Enemy_prevHP = Enemy_HP;
        }

        if (Enemy_prevHP < 0 || Enemy_prevHP == 0)
        {
            Enemy_prevHP = 0;
            GetComponent<FHK_Enemy_Chasing>().IsEnemyKeepChasing = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            transform.Translate(Vector3.down * 0.5f);
            Destroy(gameObject,1.0f);
        }

        HP_Bar.transform.localScale = new Vector3(Enemy_prevHP / Enemy_originHP,1,1);
    }
}
