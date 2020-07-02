using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingViewOption
{

}


public class LoadingView : MonoBehaviour
{


    

    public bool isProcressEnd = false;
    
    private void Update()
    {
        if(isProcressEnd)
        {
            Destroy(gameObject);
        }
    }
}
