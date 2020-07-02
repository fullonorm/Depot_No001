using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveRoomMenu : MonoBehaviour
{
    private RoomCanvases roomCanvas;

    public void FirstInitialize(RoomCanvases canvases)
    {
        roomCanvas = canvases;
    }


    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
    }

    

}
