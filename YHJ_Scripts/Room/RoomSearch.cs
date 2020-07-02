using UnityEngine;

public class RoomSearch : MonoBehaviour
{
    public GameObject Prefab;
    public Transform RoomScrollView;

    // 방 정보 만들기 PUN2 -> RoomInfo?
    //private List< > RoomList = new List< >();

    private void OnEnable()
    {
        RoomListing();
    }

    private void OnDisable()
    {
        DestroyContent();
    }

    public void RoomListing()
    {
        /* 방List<>를 순회하면서 객체 생성
        foreach ()
        {
            GameObject content = Instantiate(Prefab) as GameObject;
            content.GetComponent<RoomInformation>().method();
            content.transform.SetParent(RoomScollView);
        }
        */
    }
    
    // RESET Button Action
    // 리스트의 정보를 다시 불러온다.
    public void ResetList()
    {
        DestroyContent();
        RoomListing();
    }

    // 기존의 리스트를 전부 삭제
    private void DestroyContent()
    {
        Transform[] childList = RoomScrollView.GetComponentsInChildren<Transform>(true);
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                    Destroy(childList[i].gameObject);
            }
        }
    }

}
