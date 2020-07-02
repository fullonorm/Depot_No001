using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FHK_TmpLobby : MonoBehaviour
{
    public void FHKfunc_GoToArea01()
    {
        Debug.Log("Area01로 이동");
        SceneManager.LoadScene("Area01Desert");
    }
}
