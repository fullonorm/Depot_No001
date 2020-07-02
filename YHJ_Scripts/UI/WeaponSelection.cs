using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    public GameObject WeaponScrollView;
    public GameObject ContentPrefab;
    public GameObject EquipmentTab;
    public UIController uIController;

    private List<Weapon> weapons;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.GetInstance();
        weapons = gameManager.itemInformation.weapons;
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
        foreach(Weapon w in weapons)
        {
            GameObject content = Instantiate(ContentPrefab, transform.position, transform.rotation) as GameObject;
            content.GetComponent<W_Content>().SetContent(w, () => WeaponContentBtnListener(w.GetId()));
            content.transform.SetParent(WeaponScrollView.transform);
            content.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        }
    }

    private void DestroyContent()
    {
        Transform[] childList = WeaponScrollView.GetComponentsInChildren<Transform>(true);
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                    Destroy(childList[i].gameObject);
            }
        }
    }
    
    public void WeaponContentBtnListener(int ID)
    {
        gameManager.equipment.SetWeapon(ID);
        uIController.MoveTab(EquipmentTab);
    }
}
