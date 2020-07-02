using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FHK_Vehicle_Spec : MonoBehaviour
{
    public float Vehicle_HP;
    public float Vehicle_DEF;
    public float Vehicle_VEL;
    public float Vehicle_ACL;
    public float Vehicle_TOQ;
    public float Vehicle_BRK;

    private GameObject vehicle;
    void Start()
    {
        vehicle = GameObject.Find("Vehicle");

        FHK_Vehicle_Status vs = vehicle.gameObject.GetComponent<FHK_Vehicle_Status>();
        vs.Vehicle_HP = Vehicle_HP;
        vs.Vehicle_prevHP = Vehicle_HP;
        vs.Vehicle_originHP = Vehicle_HP;

        vs.Vehicle_DEF = Vehicle_DEF;
        vs.Vehicle_VEL = Vehicle_VEL;
        vs.Vehicle_ACL = Vehicle_ACL;
        vs.Vehicle_TOQ = Vehicle_TOQ;
        vs.Vehicle_BRK = Vehicle_BRK;
    }
}
