using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{

    [Header("RoleSelectButton")]
    public List<Button> RoleButtons;

    [Header("RefreshButton")]
    public Button RefreshButton;

    [Header("GameStartButton")]
    public Button GameStartButton;

    [Header("Photon")]
    [SerializeField]
    private Transform content;
    [SerializeField]
    private PlayerListing playerListing;

    private List<PlayerListing> listings = new List<PlayerListing>();
    private RoomCanvases roomCanvases;

    public void FirstInitialize(RoomCanvases canvases)
    {
        roomCanvases = canvases;
    }

    public override void OnEnable()
    {
        GameStartButton.interactable = false;
        base.OnEnable();
        GetCurrentRoomPlayers();
        UpdateRoleBtnState();
        ButtonCheck();
    }

    public override void OnDisable()
    {
        base.OnDisable();
        for (int i = 0; i < listings.Count; i++)
        {
            Destroy(listings[i].gameObject);
        }
        listings.Clear();
    }

    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
            return;
        foreach (KeyValuePair<int, Player> PlayerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(PlayerInfo.Value);
        }

    }
    
    public void ButtonCheck()
    {
        if ((int)PhotonNetwork.LocalPlayer.CustomProperties["role"] != -1)
            DisableRoleBtn();
        checkGameStartButton();
    }

    // content에 플레이어를 생성하는 코드 
    private void AddPlayerListing(Player player)
    {
        int index = listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            listings[index].SetPlayerInfo(player);
        }
        else
        {
            PlayerListing listing = Instantiate(playerListing, content);
            if (listing != null)
            {
                listing.SetPlayerInfo(player);
                listings.Add(listing);
            }
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        roomCanvases.CurrentRoomCanvas.LeaveRoomMenu.OnClick_LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
        checkGameStartButton();
    }

    // 떠난 플레이어 삭제 
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(listings[index].gameObject);
            listings.RemoveAt(index);
        }
    }

    // 게임 플레이 버튼 
    public void OnClick_StartGame()
    {
        string map = (string)PhotonNetwork.CurrentRoom.CustomProperties["map"];
        Hashtable table = PhotonNetwork.CurrentRoom.CustomProperties;
        table["rndRoute"] = Random.Range(0, 2);
        PhotonNetwork.CurrentRoom.SetCustomProperties(table);
        photonView.RpcSecure("RPC_SetGame", RpcTarget.All, true, PhotonNetwork.LocalPlayer);        
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel(map);      
    }

    [PunRPC]
    public void RPC_SetGame(Player player)
    {
        int index = listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            listings[index].setGame();
        }
    }

    public void Onclick_PickRole(int i)
    {
        GameManager gm = GameManager.GetInstance();
        Hashtable table = PhotonNetwork.CurrentRoom.CustomProperties;
        table[((Role)i).ToString() + "Button"] = false;
        table["Is" + ((Role)i).ToString() + "Here"] = true;
        if ((Role)i == Role.Driver)
        {
            table["Vehicle"] = gm.equipment.GetVehicle();
        }
        else
        {
            table[((Role)i).ToString()] = gm.equipment.GetWeapon();
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(table);
        photonView.RpcSecure("RPC_PickRole", RpcTarget.All, true, PhotonNetwork.LocalPlayer, (Role)i);
        DisableRoleBtn();

    }

    [PunRPC]
    public void RPC_PickRole(Player player, Role role)
    {
        int index = listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            PlayerListing playerObject = listings[index];

            playerObject.isReady = true;
            playerObject.setPlayerRole((int)role);
        }

        UpdateRoleBtnState();
        checkGameStartButton();
    }

    public void DisableRoleBtn()
    {
        foreach (Button btn in RoleButtons)
        {
            btn.interactable = false;
        }
    }

    public void UpdateRoleBtnState()
    {
        if ((int)PhotonNetwork.LocalPlayer.CustomProperties["role"] != -1)
            return;

        Debug.Log("버튼 업데이트");
        Hashtable cp = PhotonNetwork.CurrentRoom.CustomProperties;
        RoleButtons[0].interactable = (bool)cp["DriverButton"];
        RoleButtons[1].interactable = (bool)cp["Striker1Button"];
        RoleButtons[2].interactable = (bool)cp["Striker2Button"];
        RoleButtons[3].interactable = (bool)cp["Striker3Button"];
    }
    
    // 모든 유저가 역할을 하나씩 가지고 있는지 확인한다. 
    private bool checkRoleAssignedState()
    {
        foreach(PlayerListing listing in listings)
        {
            if (!listing.isReady)
                return false;
        }
        return true;
    }

    // 다시해 
    [PunRPC]
    public void ResetPosition(Player player, Role role)
    {
        
        int index = listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            PlayerListing playerObject = listings[index];
            playerObject.isReady = false;
            playerObject.setPlayerRole((int)role);
            
        }
        
        UpdateRoleBtnState();
        checkGameStartButton();
    }

    public void Onclick_ResetPosition(int i)
    {
        int? Position = (int)PhotonNetwork.LocalPlayer.CustomProperties["role"];

        if (Position == -1)
            return;
            
        Hashtable table = PhotonNetwork.CurrentRoom.CustomProperties;

        string PositionStr = ((Role)Position).ToString();
        table[PositionStr] = null;
        table["Is" + PositionStr + "Here"] = false;
        table[PositionStr + "Button"] = true;
        PhotonNetwork.CurrentRoom.SetCustomProperties(table);
         
        Debug.Log(PositionStr + "Button");
        photonView.RpcSecure("ResetPosition", RpcTarget.All, true, PhotonNetwork.LocalPlayer, (Role)i);

    }
    
    public void checkGameStartButton()
    {
        if (PhotonNetwork.IsMasterClient && checkRoleAssignedState())
            GameStartButton.interactable = true;
        else
            GameStartButton.interactable = false;
    }
}

public enum Role
{
    Nothing = -1, 
    Driver =  0,
    Striker1 = 1,
    Striker2 = 2,
    Striker3 = 3
}

