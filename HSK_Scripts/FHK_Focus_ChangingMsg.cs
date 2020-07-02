using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FHK_Focus_ChangingMsg : MonoBehaviour
{
    public string message;

    Text lensText1;

    private void Start()
    {
        //lensText1 = GameObject.Find("LensText1").GetComponent<Text>();
    }

    public void FHKfunc_ChangingLensText1(bool IsFocusing)
    {
        if (IsFocusing)
        {
            lensText1.text = message;
        }
        else
        {
            lensText1.text = "";
        }
    }
}
