using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text PlayerName;
    //[SerializeField]
    public Text PlayerRoleText;
    public Image CharatorImage;

    public Player Player { get; private set; }
    public bool isReady = false;
    public Role PlayerRole;
    public bool isFirstSetting = true;
    private int tmppid;
    private int tmpaid;

    public void SetPlayerInfo(Player player)
    {
        Player = player;
        
        Hashtable table = player.CustomProperties;
        if(!table.ContainsKey("role"))
        {
            table.Add("role", -1);
            player.SetCustomProperties(table);
        }

        PlayerRole = (Role)player.CustomProperties["role"];
        if (!PlayerRole.Equals(Role.Nothing))
        {
            isReady = true;
        }
        displayPlayerInfo();
    }
    
    public void setPlayerRole(int role)
    {
        Hashtable table = Player.CustomProperties;
        table["role"] = role;
        Player.SetCustomProperties(table);

        PlayerRole = (Role)Player.CustomProperties["role"];
        displayPlayerInfo(); 

       

    }
    
    public void displayPlayerInfo()
    {
        PlayerName.text = Player.NickName;
        GameManager gameManager = GameManager.GetInstance();
        Sprite sprite = null;
        int id;

        Hashtable table = PhotonNetwork.CurrentRoom.CustomProperties;

        if (PlayerRole == Role.Driver)
        {
            
            id = (int)table["Vehicle"];

            sprite = Resources.Load<Sprite>("Image/Vehicle" + id.ToString()) as Sprite;
            PlayerRoleText.text = PlayerRole.ToString();
        }
        else if(PlayerRole == Role.Nothing)
        {
            sprite = Resources.Load<Sprite>("Image/nothing") as Sprite;
            PlayerRoleText.text = "Unselected";
        }
        else
        {
            id = (int)table[PlayerRole.ToString()];
            sprite = Resources.Load<Sprite>("Image/Weapon" + id.ToString()) as Sprite;
            PlayerRoleText.text = PlayerRole.ToString();
        }
        CharatorImage.sprite = sprite;
    }
    

    public void setGame()
    {
        GameManager gm = GameManager.GetInstance();
        Hashtable table = PhotonNetwork.CurrentRoom.CustomProperties;
        if (PhotonNetwork.LocalPlayer.CustomProperties["role"].Equals((int)Role.Driver))
        {
            gm.WID = 0;
        }
        else
        {
            gm.WID = gm.equipment.GetWeapon();
        }

                
        if (PhotonNetwork.IsMasterClient)
            gm.IsSquadLeader = true;
        else
            gm.IsSquadLeader = false;


        gm.IsDriverHere = (bool)table["IsDriverHere"];

        if(gm.IsDriverHere)
            gm.VID = (int)table["Vehicle"];

        gm.IsStriker01Here = (bool)table["IsStriker1Here"];
        gm.IsStriker02Here = (bool)table["IsStriker2Here"];
        gm.IsStriker03Here = (bool)table["IsStriker3Here"];

        
        switch ((string)table["map"])
        {
            case "Area01Desert":
                tmpaid = 0;
                break;
        }

        gm.AID = tmpaid;

        int? pid = null;
        
        switch ((Role)PhotonNetwork.LocalPlayer.CustomProperties["role"])
        {
            case Role.Driver :
                pid = 0;
                break;
            case Role.Striker1:
                pid = 1;
                break;
            case Role.Striker2:
                pid = 2;
                break;
            case Role.Striker3:
                pid = 3;
                break;
        }
        gm.PID = (int)pid;

        gm.rndRoute = (int)table["rndRoute"];
    }



}
