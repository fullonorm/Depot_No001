using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FHK_Player_LensCanvas : MonoBehaviour
{
    public float PointerTimerfloat = 0;
    private float PrePointerTimerfloat = 0;
    private float CheckFloat = 0;

    private GameObject vehicle;
    public GameObject hpDisplayObj;

    public Text systemMsg;

    private void Start()
    {
        vehicle = GameObject.Find("Vehicle");
        //downText = FHK_StageManager.lensText;
    }
    
    void Update()
    {
        systemMsg.text = FHK_StageManager.systemMsg;
        CheckFloat += Time.deltaTime * 10;
        if (CheckFloat > 1)
        {
            CheckFloat = 0;
            PrePointerTimerfloat = PointerTimerfloat;
        }
        if (PrePointerTimerfloat == PointerTimerfloat) PointerTimerfloat = 0;

        if(FHK_StageManager.IsStageOngoing)
            hpDisplayObj.transform.localScale = new Vector3(vehicle.GetComponent<FHK_Vehicle_Status>().Vehicle_HP_Rate, 1, 1);
    }
}
