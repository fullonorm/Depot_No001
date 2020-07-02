using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviourPunCallbacks
{

    [Header("MapImages")]
    public Image Map1;

    [Header("MapNames")]
    public Text[] Map_Names;

    [Header("Selected Map")]
    public Image SelectedMap;
    public Text SelectedMapName;

    [Header("InputField")]
    public InputField RoomName;

    [Header("CreateButton")]
    public Button CreateButton;

    private string selectedMap = null;
    
    private string[] map_Name =
    {
        "Area01Desert",
        "Area02XXX",
        "Area03XXX",
        "Area04XXX"
    };

    void Start()
    {
        CreateButton.interactable = false;

        for(int i = 0; i < Map_Names.Length; i++)
        {
            Map_Names[i].text = map_Name[i];
        }
    }

    void Update()
    {
        checkOption();
    }

    public void setMap(int map_Number)
    {
        if (map_Number < 0 && map_Number >= map_Name.Length)
            return;

        string name = map_Name[map_Number];

        selectedMap = name;
        SelectedMapName.text = name;
    }
    
    public void SelectMap(Image Map)
    {
        Sprite sprite = Map.sprite;
        SelectedMap.sprite = sprite;
    }
        
    public void checkOption()
    {
        if (RoomName.text != "" && selectedMap != null)
            CreateButton.interactable = true;
    }

    public string getSelectedMap()
    {
        return selectedMap;
    }
    
}
