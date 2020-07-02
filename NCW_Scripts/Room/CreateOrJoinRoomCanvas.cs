using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOrJoinRoomCanvas : MonoBehaviour
{
    [SerializeField]
    private CreateRoomMenu createRoomMenu;
    [SerializeField]
    private RoomListingMenu roomListingMenu;
    private RoomCanvases roomCanvasses;

    public void FirstInitalize(RoomCanvases canvases)
    {
        roomCanvasses = canvases;
        createRoomMenu.FirstInitalize(canvases);
        roomListingMenu.FirstInitalize(canvases);
    }
}
