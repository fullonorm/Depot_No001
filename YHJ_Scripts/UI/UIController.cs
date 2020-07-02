using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{   
    [Header("UI Tabs")]
    public List<GameObject> UI_Tabs;
    
    public void MoveTab(GameObject Tab)
    {
        foreach (GameObject t in UI_Tabs)
        {   
            t.SetActive(false);
        }
        Tab.SetActive(true);
    }

    void Start()
    {
       
    }

    public void joinLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    public void ItemWrite()
    {
        Database.GetInstance().ItemDataWrite();
        
    }

    public void returnGameRoom()
    {
        MoveTab(UI_Tabs[7]);
    }
}

public enum Tabs
{
    Standard = 0,
    Equipment = 1,
    Weapon = 2,
    Vehicle = 3,
    Material = 4,
    RoomSearch = 5,
    RoomCreate = 6,
    GameRoom = 7
}



