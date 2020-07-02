using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FHK_Auto_Driver_Area01 : MonoBehaviour
{
    public bool IsAutoMode;
    public GameObject vehicle;

    public float overTime;
    float timerFloat;

    public int routeNumber;
    public int progress = 0;
    public Transform point_00;
    public Transform point_01;
    public Transform point_02;
    public Transform point_03;

    [Header("First Route")]
    public Transform point_04;
    public Transform point_05;
    public Transform point_06;
    public Transform point_07;

    [Header("Destination")]
    public Transform point_08;

    [Header("First Route")]
    public Transform point_09;
    public Transform point_10;
    public Transform point_11;
    public Transform point_12;

    void Start()
    {
        IsAutoMode = !FHK_StageManager.instance.IsDriverHere;
    }

    void FHKfunc_MoveToNextPoint(Transform point_n)
    {
        vehicle.transform.position = Vector3.MoveTowards(vehicle.transform.position, point_n.position, 0.05f);
        //vehicle.transform.LookAt(point_n);

        Vector3 dir = point_n.position - vehicle.transform.position;
        vehicle.transform.rotation = Quaternion.Lerp(vehicle.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime);

    }

    void Update()
    {
        if (FHK_StageManager.IsStageOngoing)
        {
            timerFloat += Time.deltaTime;
            if (timerFloat > overTime)
            {
                //Debug.Log("핸들압수");
                IsAutoMode = true;
            }


            if (IsAutoMode)
            {
                if (routeNumber == 0)
                {
                    switch (progress)
                    {
                        case 0: FHKfunc_MoveToNextPoint(point_00); break;
                        case 1: FHKfunc_MoveToNextPoint(point_01); break;
                        case 2: FHKfunc_MoveToNextPoint(point_02); break;
                        case 3: FHKfunc_MoveToNextPoint(point_03); break;
                        case 4: FHKfunc_MoveToNextPoint(point_04); break;
                        case 5: FHKfunc_MoveToNextPoint(point_05); break;
                        case 6: FHKfunc_MoveToNextPoint(point_06); break;
                        case 7: FHKfunc_MoveToNextPoint(point_07); break;
                        case 8: FHKfunc_MoveToNextPoint(point_08); break;
                    }
                }
                if (routeNumber == 1)
                {
                    switch (progress)
                    {
                        case 0: FHKfunc_MoveToNextPoint(point_00); break;
                        case 1: FHKfunc_MoveToNextPoint(point_01); break;
                        case 2: FHKfunc_MoveToNextPoint(point_02); break;
                        case 3: FHKfunc_MoveToNextPoint(point_03); break;
                        case 4: FHKfunc_MoveToNextPoint(point_09); break;
                        case 5: FHKfunc_MoveToNextPoint(point_10); break;
                        case 6: FHKfunc_MoveToNextPoint(point_11); break;
                        case 7: FHKfunc_MoveToNextPoint(point_12); break;
                        case 8: FHKfunc_MoveToNextPoint(point_08); break;
                    }
                }

            }
        }
        if (FHK_StageManager.IsStageCleared)
        {
            FHKfunc_MoveToNextPoint(point_08);
        }

        
    }
}
