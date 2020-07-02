using System.Collections.Generic;
using UnityEngine;

public class VehicleSelection : MonoBehaviour
{
    public GameObject VihicleScrollView;
    public GameObject ContentPrefab;
    public GameObject EquipmentTab;
    public UIController uIController;

    private List<Vehicle> vehicles;
    private GameManager gameManager;
    

    private void Awake()
    {
        gameManager = GameManager.GetInstance();
        vehicles = gameManager.itemInformation.vehicles;
    }

    private void OnEnable()
    {
        DisplayScrolView();
    }

    private void OnDisable()
    {
        DestroyContent();
    }

    public void DisplayScrolView()
    {
        foreach (Vehicle v in vehicles)
        {
            GameObject content = Instantiate(ContentPrefab, transform.position, transform.rotation) as GameObject;
            content.GetComponent<V_Content>().SetContent(v, () => VehicleContentBtnListener(v.GetId()));
            content.transform.SetParent(VihicleScrollView.transform);
            content.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    private void DestroyContent()
    {
        Transform[] childList = VihicleScrollView.GetComponentsInChildren<Transform>(true);
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                    Destroy(childList[i].gameObject);
            }
        }
    }

    private void VehicleContentBtnListener(int ID)
    {
        gameManager.equipment.SetVehicle(ID);
        uIController.MoveTab(EquipmentTab);
    }

}

