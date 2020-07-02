using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform content;
    [SerializeField]
    private RoomListing roomListing;

    private List<RoomListing> listings = new List<RoomListing>();
    private RoomCanvases roomCanvases;
    
    public void FirstInitalize(RoomCanvases canvases)
    {
        roomCanvases = canvases;
    }

    public override void OnJoinedRoom()
    {
        roomCanvases.CurrentRoomCanvas.Show();
        DestroyContent();
        listings.Clear();
        gameObject.SetActive(false);
    }

    public void DestroyContent()
    {
        Transform[] childList = content.GetComponentsInChildren<Transform>(true);
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                    Destroy(childList[i].gameObject);
            }
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                int index = listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if(index != -1)
                {
                    Destroy(listings[index].gameObject);
                    listings.RemoveAt(index);
                }
            }
            else
            {
                int index = listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index == -1)
                {
                    RoomListing listing = Instantiate(roomListing, content);
                    if (listing != null)
                    {
                        listing.SetRoomInfo(info);
                        listings.Add(listing);
                    }
                }
            }
        }
    }

}
