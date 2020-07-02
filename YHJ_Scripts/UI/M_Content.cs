using UnityEngine;
using UnityEngine.UI;

public class M_Content : MonoBehaviour
{
    public Image image;
    public Text name;
    public Text amount;
    public Button button;
    private CombinationController combinationController;

    public void SetContent(Item m, int amount, CombinationController combinationController, System.Action resetAction)
    {
        ItemType type = m.GetItemType();
        int id = m.GetId();
        image.sprite = Resources.Load<Sprite>("Image/" + type.ToString() + id) as Sprite;  
        name.text = m.GetName();
        this.amount.text = amount.ToString();
        this.combinationController = combinationController;

        button.onClick.AddListener(() => {
            combinationController.setItemId(id);
            string title = "확인";
            string message = m.GetName();

            AlertViewController2.Show(title, message, new AlertViewOptions2
            {
                cancelButtonTitle = "취소",
                itemType = type,
                combinationButtonDelegate = () =>
                {
                    combinationController.combination();
                    resetAction();
                },
                decombinationButtonDelegate = () =>
                {
                    combinationController.decombination();
                    resetAction();
                },

                CombinationController = combinationController
            });
        });
    }

    

}
