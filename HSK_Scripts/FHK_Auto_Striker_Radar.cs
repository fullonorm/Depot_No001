using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FHK_Auto_Striker_Radar : MonoBehaviour
{
    public GameObject AutoStriker;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            gameObject.SetActive(false);
            AutoStriker.transform.LookAt(new Vector3(other.transform.position.x, 1.2f, other.transform.position.z));
            AutoStriker.GetComponent<FHK_Auto_Striker>().IsFocusingOn = true;
        }
    }
}
