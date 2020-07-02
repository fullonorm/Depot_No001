using UnityEngine;
using UnityEngine.UI;

public class EquipmentImage : MonoBehaviour
{
    public Image weaponImage;
    public Image vehicleImage;
    private Equipment equipment;
    private ItemInformation itemInformation;

    public Text weaponInfoText;
    public Text vehicleInfoText;

    void Awake()
    {
        equipment = GameManager.GetInstance().equipment;
        itemInformation = GameManager.GetInstance().itemInformation;
    }

    private void OnEnable()
    {
        // 지금 사용하고 있는 장비
        weaponImage.sprite = Resources.Load<Sprite>("Image/Weapon" + equipment.GetWeapon()) as Sprite;
        vehicleImage.sprite = Resources.Load<Sprite>("Image/Vehicle" + equipment.GetVehicle()) as Sprite;
        
        Weapon w = itemInformation.GetWeapon(equipment.GetWeapon());
        Vehicle v = itemInformation.GetVehicle(equipment.GetVehicle());


        string weaponInfo =
            "DMG : " + w.GetDamage().ToString() + " \n" +
            "RNG : " + w.GetRange().ToString() + " \n" +
            "FPS : " + w.GetROF().ToString() + " \n";

        string vehicleInfo =
            "HP : " + v.GetHp().ToString() + " \n" +
            "DEF : " + v.GetDef().ToString() + "\n" +
            "ACC : " + v.GetAcceleration().ToString() + "\n" +
            "MVL : " + v.GetVelocity().ToString() + "\n";

        weaponInfoText.text = weaponInfo.Replace("\\n", "\n");
        vehicleInfoText.text = vehicleInfo.Replace("\\n", "\n");
    }
}
