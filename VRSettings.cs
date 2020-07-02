using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRSettings : MonoBehaviour
{
    public bool VROn;
    // Start is called before the first frame update
    private void Awake()
    {
        //Application.targetFrameRate = 40;
        //Screen.orientation = ScreenOrientation.Portrait;
    }

    void Start()
    {
        if (VROn)
        {
            StartCoroutine(VRSetting("Cardboard", true));

            //XRSettings.LoadDeviceByName("Cardboard");
            //XRSettings.enabled = true;
        }
        else
        {
            StartCoroutine(VRSetting("None", false));

            //XRSettings.LoadDeviceByName("None");
            //XRSettings.enabled = false;
        }
    }
    void FHKfunc_ActingAfterFewSeconds()
    {
        
    }

    public void VRSettingOff()
    {
        StartCoroutine(VRSetting("None", false));
    }
    
    // VRSetting
    IEnumerator VRSetting(string sdkName, bool flag)
    {
        XRSettings.LoadDeviceByName(sdkName);
        yield return new WaitForSeconds(0.01f);
        XRSettings.enabled = flag;
    }
}
