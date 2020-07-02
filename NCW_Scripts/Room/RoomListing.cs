using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
    [SerializeField] 
    private Text roomName;
    public Text maxPlayerCount;
    public Text currentPlayerCount;
    public Image roomMapImage;

    public RoomInfo RoomInfo { get; private set; }

    private GameObject RoomSearch;
    
    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        roomName.text = roomInfo.Name;
        maxPlayerCount.text = roomInfo.MaxPlayers.ToString();
        currentPlayerCount.text = roomInfo.PlayerCount.ToString();
        string map = (string)roomInfo.CustomProperties["map"];
        Sprite sprite = Resources.Load<Sprite>("Image/" + map) as Sprite;
        roomMapImage.sprite = sprite;
    }
    
    public void OnClick_Button()
    {
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }
}
