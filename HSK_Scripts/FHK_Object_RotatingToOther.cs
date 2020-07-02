using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FHK_Object_RotatingToOther : MonoBehaviour
{
    public GameObject Other;

    void Update()
    {
        //Debug.Log(Other.transform.rotation.eulerAngles.y);
        gameObject.transform.rotation = Quaternion.Euler(0, Other.transform.rotation.eulerAngles.y, 0);
    }
}
