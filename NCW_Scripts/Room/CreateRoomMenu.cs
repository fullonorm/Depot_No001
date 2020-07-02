using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text roomName;

    private RoomCanvases roomCanvases;

    public void FirstInitalize(RoomCanvases canvases)
    {
        roomCanvases = canvases;
    }

    public void Onclick_CreateRoom()
    { 
        if (!PhotonNetwork.IsConnected)
            return;

        RoomOptions options = new RoomOptions();
        options.BroadcastPropsChangeToAll = true;
        options.PublishUserId = true;
        options.MaxPlayers = 4;

        string map = gameObject.GetComponent<CreateRoom>().getSelectedMap();
        
        // 커스텀 
        Hashtable cp = new Hashtable();
        cp.Add("map", map);

        cp.Add("rndRoute", null);

        cp.Add("Vehicle", null);
        cp.Add("Striker1", null);
        cp.Add("Striker2", null);
        cp.Add("Striker3", null);

        cp.Add("IsDriverHere", false);
        cp.Add("IsStriker1Here", false);
        cp.Add("IsStriker2Here", false);
        cp.Add("IsStriker3Here", false);

        cp.Add("DriverButton", true);
        cp.Add("Striker1Button", true);
        cp.Add("Striker2Button", true);
        cp.Add("Striker3Button", true);

        options.CustomRoomProperties = cp;
        options.CustomRoomPropertiesForLobby = new string[] { "map" };

        PhotonNetwork.JoinOrCreateRoom(roomName.text, options, TypedLobby.Default);        
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully", this);
        roomCanvases.CurrentRoomCanvas.Show();
        gameObject.SetActive(false); 
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("room failed" + message, this);
    }
    
    public void FinishButton()
    {
        AlertViewController.Show("확인", "게임방을 개설합니다.", new AlertViewOptions
        {
            cancelButtonDelegate = () =>
            {
                // 입력된 정보 초기화
                //roomName.text = "";
                
                Debug.Log("취소");
            },
            okButtonDelegate = () =>
            {
                // 확인 절차 후에 게임방 생성
                Onclick_CreateRoom();
                Debug.Log("확인");
            }
        });
    }
}
