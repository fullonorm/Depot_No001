using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForTest : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 5;
        // 초당 프레임 
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "0.0.0";
        PhotonNetwork.ConnectUsingSettings();

        // 랜덤 닉네임 부여 
        PhotonNetwork.NickName = randomtext(4);
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to Server!!");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnted from server for return" + cause.ToString());
    }

    public string randomtext(int strLen)
    {
        int rnum = 0;
        int i, j;
        string ranStr = null;

        System.Random ranNum = new System.Random();

        for (i = 0; i <= strLen; i++)
        {
            for (j = 0; j <= 122; j++)
            {
                rnum = ranNum.Next(48, 123);
                if (rnum >= 48 && rnum <= 122 && (rnum <= 57 || rnum >= 65) && (rnum <= 90 || rnum >= 97))
                {
                    break;
                }
            }

            ranStr += Convert.ToChar(rnum);
        }

        return ranStr;
    }
    


}
