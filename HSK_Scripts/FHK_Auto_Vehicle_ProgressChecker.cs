using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FHK_Auto_Vehicle_ProgressChecker : MonoBehaviour
{
    public GameObject StageManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Point")
        {
            other.gameObject.SetActive(false);

            StageManager.GetComponent<FHK_Auto_Driver_Area01>().progress++;
        }
    }
}
