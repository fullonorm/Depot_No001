using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AlertViewOptions2
{
    // 취소 버튼의 타이틀 
    public string cancelButtonTitle;
    // 취소 버튼을 눌렀을 때 실행되는 대리자
    public System.Action cancelButtonDelegate;
    // 조합 버튼을 눌렀을 때 실행되는 대리자
    public System.Action combinationButtonDelegate;
    // 분해 버튼을 눌었을 때 실행되는 대리자 
    public System.Action decombinationButtonDelegate;

    public ItemType itemType;

    public CombinationController CombinationController;
}

public class AlertViewController2 : MonoBehaviour
{
    [Header("AlertView2")]
    // 타이틀을 표시 
    public Text titleLabel;
    // 메시지를 표시 
    public Text messageLabel;
    // 취소버튼
    public Button cancelButton;
    // 조합 버튼 
    public Button combinatinoButton;
    // 분해 버튼
    public Button decombinationButton;

    private static GameObject prefab;
    // 취소 버튼 클릵시 실행되는 대리자 지정
    private System.Action cancelButtonDelegate;
    // 조합 버튼을 눌렀을 때 실행되는 대리자
    public System.Action combinationButtonDelegate;
    // 분해 버튼을 눌었을 때 실행되는 대리자 
    public System.Action decombinationButtonDelegate;

    private CombinationController combinationController;

    [Header("Window2")]
    public GameObject Window2;
    public Text TitleLabel2;
    public Text MessageLabel2;
    public Transform MaterialDisplayArea;
    public Image imagePrefab;

    public Button okButton;

    public System.Action finishButtonDelegate;

    private bool isCombination;
    private ItemType itemType;
    private bool isFinishable;

    // 알임 뷰를 표시하는 static 메서드 
    public static AlertViewController2 Show(
        string title, string message, AlertViewOptions2 options = null)
    {
        if (prefab == null)
        {
            // 프리팹을 읽어 들인다. 
            prefab = Resources.Load("AlertView2") as GameObject;
        }

        GameObject go = Instantiate(prefab) as GameObject;
        AlertViewController2 alertView = go.GetComponent<AlertViewController2>();
        alertView.UpadateContent(title, message, options);

        return alertView;
    }

    // 읽어 들인 내용을 갱신하는 메서드 
    public void UpadateContent(
        string title, string message, AlertViewOptions2 options = null)
    {
        if (options != null)
        {
            // 표시 옵션이 있을 때 옵션의 내용에 맞춰 버튼을 표시하거나 표시하지 않는다.
            cancelButton.transform.gameObject.SetActive(options.cancelButtonTitle != null);

            cancelButton.gameObject.SetActive(options.cancelButtonTitle != null);
            cancelButtonDelegate = options.cancelButtonDelegate;

            combinationButtonDelegate = options.combinationButtonDelegate;
            decombinationButtonDelegate = options.decombinationButtonDelegate;

            combinationController = options.CombinationController;

            itemType = options.itemType;

            message += "아이템은";

            if (options.itemType == ItemType.Material)
            {
                combinatinoButton.interactable = false;
                decombinationButton.interactable = false;

                message += " 조합 및 분해가 불가능한 아이템입니다. \n";
            }
            else
            {
                if (combinationController.getRecipe().checkRecipe(combinationController.selectedItemId))
                {
                    bool f1 = combinationController.checkCombination();
                    bool f2 = combinationController.checkDecombination();

                    if (f1 && f2)
                        message += " 조합 및 분해가 가능한 아이텝입니다. \n";

                    else if (!f1 && f2)
                    {
                        //combinatinoButton.interactable = false;
                        isFinishable = false;
                        combinatinoButton.interactable = true;
                        message += "재료의 수량이 부족하여 조합은 하지 못하지만 분해는 가능합니다.";
                    }
                    else if (f1 && !f2)
                    {
                        decombinationButton.interactable = false;
                        message += "수량이 1개 이하이기 때문에 분해는 못하지만 조합은 가능합니다.";
                    }
                    else
                    {
                        message += " 재료의 수량이 부족하거나 아이템의 개수가 1개 이하이기 때문에 조합 및 분해가 불가능합니다. \n";
                        //combinatinoButton.interactable = false;
                        isFinishable = false;
                        combinatinoButton.interactable = true;
                        decombinationButton.interactable = false;
                    }

                    //if (!f1)
                    //{
                    //    combinatinoButton.interactable = false;
                    //    message += " 재료의 수량이 부족합니다. \n";
                    //}

                    //if (!f2)
                    //{ 
                    //    decombinationButton.interactable = false;
                    //    message += " 수량이 1개 이하이면 분해할 수 없습니다. \n";
                    //}


                }
                else
                {
                    message += " 조합 및 분해가 불가능한 아이템입니다. \n";
                    combinatinoButton.interactable = false;
                    decombinationButton.interactable = false;
                }
            }
        }
        // options = null인 경우 
        else
        {
            // 표시 옵션이 지정된 경우 기본 버튼을 표시한다.
            cancelButton.gameObject.SetActive(true);
            combinatinoButton.interactable = false;
            decombinationButton.interactable = false;
        }

        //타이틀과 메시지를 설정 
        titleLabel.text = title;
        messageLabel.text = message.Replace("\\n", "\n");
    }

    //Window2 설정
    public void UpdateWindow2()
    {
        int loss = 0;

        if (isCombination)
        {
            if (combinationController.checkCombination())
                isFinishable = true;
            loss = 0;
            TitleLabel2.text = "소모되는 아이템";
            MessageLabel2.text = "";
        }

        else
        {
            isFinishable = true;
            loss = -1;
            MessageLabel2.text = "분해는 약간의 손실이 있습니다.";
            TitleLabel2.text = "생성되는 아이템";
        }

        okButton.interactable = isFinishable;

        ItemInformation info = GameManager.GetInstance().itemInformation;

        foreach (KeyValuePair<int, int> item in combinationController.getRecipeComponent())
        {
            Image image = Instantiate(imagePrefab);
            image.transform.SetParent(MaterialDisplayArea);
            image.transform.localScale = Vector3.one;
            image.sprite = Resources.Load<Sprite>("Image/" + info.GetItemType(item.Key).ToString() + item.Key) as Sprite;

            Debug.Log("Image/" + info.GetItemType(item.Key).ToString() + item.Key);

            image.transform.GetChild(0).GetComponent<Text>().text = (item.Value + loss).ToString();
        }
    }

    // 알림창을 닫는 메서드 
    public void Dismiss()
    {
        Destroy(gameObject);
    }

    //취소 버튼을 눌렀을 때 호출되는 메서드 
    public void OnPressCancelButton()
    {
        if (cancelButtonDelegate != null)
        {
            cancelButtonDelegate.Invoke();
        }
        Dismiss();
    }

    public void OnPressCombinationButton()
    {
        isCombination = true;
        Window2.SetActive(true);
        UpdateWindow2();
    }

    public void OnPressDecombinationButton()
    {
        isCombination = false;
        Window2.SetActive(true);
        UpdateWindow2();
    }

    // 최종확인 버튼
    public void OnPressOkButton()
    {
        if (isCombination)
        {
            finishButtonDelegate = combinationButtonDelegate;
        }
        else
        {
            finishButtonDelegate = decombinationButtonDelegate;
        }

        if (finishButtonDelegate != null)
            finishButtonDelegate.Invoke();

        Dismiss();
    }

    public void OnPressBackButton()
    {
        Window2.SetActive(false);
        Transform[] childList = MaterialDisplayArea.GetComponentsInChildren<Transform>(true);
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