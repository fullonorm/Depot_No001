using UnityEngine;
using UnityEngine.UI;

public class V_Content : MonoBehaviour
{
    public Image image;
    public Text name;
    public Text hp;
    public Text def;
    public Text vel;
    public Text acl;
    public Text toq;
    public Button button;

    public void SetContent(Vehicle v, System.Action action)
    {
        image.sprite = Resources.Load<Sprite>("Image/Vehicle" + v.GetId()) as Sprite;
        name.text = v.GetName();
        hp.text = v.GetHp().ToString();
        def.text = v.GetDef().ToString();
        vel.text = v.GetVelocity().ToString();
        acl.text = v.GetAcceleration().ToString();
        toq.text = v.GetTorque().ToString();

        button.onClick.AddListener(() => action());
        if (GameManager.GetInstance().itemBox.GetItemAmount(v.GetId()) <= 0)
            button.interactable = false;
    }
}
