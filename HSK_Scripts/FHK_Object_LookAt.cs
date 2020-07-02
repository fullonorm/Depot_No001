using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FHK_Object_LookAt : MonoBehaviour
{
    public Transform tf;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(tf);
    }
}
