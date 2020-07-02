using UnityEngine;
using UnityEngine.UI;

public class W_Content : MonoBehaviour
{
    public Image image;
    public Text name;
    public Text damage;
    public Text FPS;
    public Text RNG;
    public Button button;

    public void SetContent(Weapon w, System.Action action)
    {
        image.sprite = Resources.Load<Sprite>("Image/Weapon" + w.GetId()) as Sprite;
        name.text = w.GetName();
        damage.text = w.GetDamage().ToString();
        FPS.text = w.GetROF().ToString();
        RNG.text = w.GetRange().ToString();

        button.onClick.AddListener(() => action());
        if (GameManager.GetInstance().itemBox.GetItemAmount(w.GetId()) <= 0)
            button.interactable = false;
    }
}
