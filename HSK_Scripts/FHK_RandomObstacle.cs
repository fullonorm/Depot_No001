using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FHK_RandomObstacle : MonoBehaviour
{
    private int obstacleCase;
    public GameObject Obstacle01;
    public GameObject Obstacle02;
    public GameObject StageManager;
    void Start()
    {
        obstacleCase = Random.Range(0, 2);
        StageManager.GetComponent<FHK_Auto_Driver_Area01>().routeNumber = obstacleCase;

        // 1이라면 두번째 장애물 제거
        if (obstacleCase > 0)
        {
            Destroy(Obstacle02);
        }
        // 0이라면 첫번째 장애물 제거
        else
        {
            Destroy(Obstacle01);
        }
    }
}
