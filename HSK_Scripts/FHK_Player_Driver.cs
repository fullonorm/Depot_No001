using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FHK_Player_Driver : MonoBehaviourPun
{
    public GameObject privateCamera;
    public GameObject DriverCamera;
    private GameObject vehicle; // 차량오브젝트
    private GameObject handle; // 보여주기식 핸들

    private bool IsMovingToForward;
    private bool IsMovingToBack;
    private bool IsMovingToRight;
    private bool IsMovingToLeft;
    
    private float moveFloat;

    private float v_vel;
    private float v_acl;
    private float v_toq;
    private float v_brk;

    private float v_velNow;
    private float v_toqNow;

    private void Start()
    {
        vehicle = GameObject.Find("Vehicle");
        handle = GameObject.Find("Handle");

        Invoke("FHK_slowStart", 0.5f);

        if (!photonView.IsMine)
        {
            privateCamera.SetActive(false);
        }
    }

    void FHK_slowStart()
    {
        FHK_Vehicle_Status vs = vehicle.gameObject.GetComponent<FHK_Vehicle_Status>();
        v_velNow = 0;
        v_vel = vs.Vehicle_VEL;
        v_acl = vs.Vehicle_ACL;
        v_toq = vs.Vehicle_TOQ;
        v_brk = vs.Vehicle_BRK;
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        if (FHK_StageManager.IsStageOngoing)
        {
            // vehicle 이동
            vehicle.transform.Translate(Vector3.forward * Time.deltaTime * v_velNow);

            // handle 회전
            handle.gameObject.transform.rotation =
                Quaternion.Euler(handle.gameObject.transform.rotation.eulerAngles.x,
                handle.gameObject.transform.rotation.eulerAngles.y,
                -DriverCamera.transform.rotation.eulerAngles.y);

            // 전진이동쿼드와 상호작용
            if (IsMovingToForward) 
            {
                if (IsMovingToRight)
                {
                    // 우측으로 도는 힘 추가
                    vehicle.transform.Rotate(0, v_toqNow, 0);

                    v_toqNow += v_acl * 0.1f * Time.deltaTime;
                    if (v_toqNow > v_toq) v_toqNow = v_toq;
                }
                else if (IsMovingToLeft)
                {
                    // 좌측으로 도는 힘 추가
                    vehicle.transform.Rotate(0, -v_toqNow, 0);

                    v_toqNow += v_acl * 0.1f * Time.deltaTime;
                    if (v_toqNow > v_toq) v_toqNow = v_toq;
                }

                v_velNow += v_acl * Time.deltaTime;
                if (v_velNow > v_vel) v_velNow = v_vel;

                //Debug.Log(v_velNow + " / " + v_toqNow);
            }

            // 후진이동쿼드와 상호작용
            else if (IsMovingToBack)
            {
                if (IsMovingToRight)
                {
                    // 좌측으로 도는 힘 추가
                    vehicle.transform.Rotate(0, -v_toqNow, 0);

                    v_toqNow += v_acl * 0.1f * Time.deltaTime;
                    if (v_toqNow > v_toq) v_toqNow = v_toq;
                }
                else if (IsMovingToLeft)
                {
                    // 우측으로 도는 힘 추가
                    vehicle.transform.Rotate(0, v_toqNow, 0);

                    v_toqNow += v_acl * 0.1f * Time.deltaTime;
                    if (v_toqNow > v_toq) v_toqNow = v_toq;
                }

                v_velNow -= v_acl * Time.deltaTime;
                if (v_velNow < -v_vel) v_velNow = -v_vel;

                Debug.Log(v_velNow + " / " + v_toqNow);
            }

            // 어떤 이동쿼드와도 상호작용하지 않을 경우
            else
            {
                v_toqNow = 0;

                if(v_velNow > 0)
                {
                    v_velNow -= v_brk;
                    if(v_velNow <= 0) v_velNow = 0;
                }
                if (v_velNow < 0)
                {
                    v_velNow += v_brk;
                    if (v_velNow >= 0) v_velNow = 0;
                }
            }

            
            
        }

        // 게임진행중이 아니라면?
        else{  }
    }
    
    // event trigger
    public void FHKfunc_MoveTo(string direction)
    {
        switch (direction)
        {
            default:
                IsMovingToForward = false;
                IsMovingToBack = false;
                IsMovingToRight = false;
                IsMovingToLeft = false;
                break;
            case "Forward":
                IsMovingToForward = true;
                break;
            case "Back":
                IsMovingToBack = true;
                break;
            case "Right":
                IsMovingToRight = true;
                break;
            case "Left":
                IsMovingToLeft = true;
                break;
        }
    }
}
