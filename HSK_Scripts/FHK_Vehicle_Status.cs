using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FHK_Vehicle_Status : MonoBehaviour
{
    public float Vehicle_HP;
    public float Vehicle_prevHP;
    public float Vehicle_originHP;

    public float Vehicle_HP_Rate;

    public float Vehicle_DEF;

    public float Vehicle_VEL;
    public float Vehicle_ACL;
    public float Vehicle_TOQ;
    public float Vehicle_BRK;
    
    private void Update()
    {
        if (Vehicle_prevHP < 0)
        {
            FHK_StageManager.IsStageEnd = true;
            Destroy(this);
        }

        if (Vehicle_prevHP > Vehicle_HP)
        {
            Vehicle_prevHP -= Time.deltaTime * Mathf.Abs(Vehicle_HP);
            if (Vehicle_prevHP < Vehicle_HP) Vehicle_prevHP = Vehicle_HP;
        }

        Vehicle_HP_Rate = Vehicle_prevHP / Vehicle_originHP;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Destination")
        {
            FHK_StageManager.IsStageCleared = true;
            FHK_StageManager.IsStageEnd = true;
        }
    }
}
