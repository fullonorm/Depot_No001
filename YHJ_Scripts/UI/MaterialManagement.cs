using System.Collections.Generic;
using UnityEngine;

public class MaterialManagement : MonoBehaviour
{
    public GameObject MaterialScollView;
    public GameObject ItemPrefab;
    public CombinationController combinationController;

    private List<Item> materials;
    private ItemBox itemBox;
    private GameManager gameManager;
    
    private void Awake()
    {
        gameManager = GameManager.GetInstance();
        itemBox = gameManager.itemBox;
        materials = gameManager.itemInformation.materials;
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
        foreach (Item m in materials)
        {
            GameObject content = Instantiate(ItemPrefab, transform.position, transform.rotation) as GameObject;
            content.GetComponent<M_Content>().SetContent(m, itemBox.GetItemAmount(m.GetId()), combinationController, ResetContent);
            content.transform.SetParent(MaterialScollView.transform);
            content.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    private void DestroyContent()
    {
        Transform[] childList = MaterialScollView.GetComponentsInChildren<Transform>(true);
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                    Destroy(childList[i].gameObject);
            }
        }
    }

    private void ResetContent()
    {
        DestroyContent();
        DisplayScrolView();
    }
}