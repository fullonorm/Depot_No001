using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentRoomCanvas : MonoBehaviour
{
    [SerializeField]
    private PlayerListingsMenu playerListingMenu;
    [SerializeField]
    private LeaveRoomMenu leaveRoomMenu;
    private RoomCanvases roomCanvases;

    public LeaveRoomMenu LeaveRoomMenu { get { return leaveRoomMenu; } }

    public void FirstInitalize(RoomCanvases canvases)
    {
        roomCanvases = canvases;
        playerListingMenu.FirstInitialize(canvases);
        leaveRoomMenu.FirstInitialize(canvases);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);

    }
}
