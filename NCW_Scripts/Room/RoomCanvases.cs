using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCanvases : MonoBehaviour
{
    [SerializeField]
    private CreateOrJoinRoomCanvas createOrJoinRoomCanvas;
    public CreateOrJoinRoomCanvas CreateOrJoinRoomCanvas { get { return createOrJoinRoomCanvas; } }

    [SerializeField]
    private CurrentRoomCanvas currentRoomCans;
    public CurrentRoomCanvas CurrentRoomCanvas { get { return currentRoomCans; } }

    private void Awake()
    {
        FirstInitialize();
    }
    private void FirstInitialize()
    {
        CreateOrJoinRoomCanvas.FirstInitalize(this);
        CurrentRoomCanvas.FirstInitalize(this);
    }
}
