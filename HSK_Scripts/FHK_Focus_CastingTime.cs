using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FHK_Focus_CastingTime : MonoBehaviour
{
    public bool IsRepeat = false;
    public int GimmickNumber = 1;

    public string beforeMsg = "";
    public string afterMsg = "";
    public float castingTime = 2.0f;

    Text lensText1;
    float timerFloat = 0;
    bool IsTimerRun = false;

    private void Start()
    {
        //lensText1 = GameObject.Find("LensText1").GetComponent<Text>();
    }

    private void Update()
    {
        if (IsTimerRun)
        {
            if(!IsRepeat) lensText1.text = beforeMsg;
            timerFloat += Time.deltaTime / castingTime;

            //FHK_Player_LensCanvas.PointerTimerfloat = timerFloat;

            if (timerFloat > 1)
            {
                timerFloat = 0;
                IsTimerRun = false;
                FHKfunc_GimmickWorkSwitch(GimmickNumber);

                if (IsRepeat) IsTimerRun = true;
            }
        }
        else timerFloat = 0;
    }

    private void FHKfunc_GimmickWorkSwitch(int GimmickNumber)
    {
        switch (GimmickNumber)
        {
            case 0:
                break;

            case 1: // afterMsg 출력
                lensText1.text = afterMsg;
                break;

            case 2:
                lensText1.text = "2번 동작 완료";
                break;

            case 3:
                lensText1.text = "3번 동작 완료";
                break;
        }
    }

    public void FHKfunc_GimmickWork(bool IsFocusing)
    {
        if (IsFocusing)
        {
            IsTimerRun = true;
        }
        else
        {
            IsTimerRun = false;
            lensText1.text = "";

            //FHK_Player_LensCanvas.PointerTimerfloat = 0;
        }
    }
}
