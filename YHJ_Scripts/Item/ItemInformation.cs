using System.Collections.Generic;

public class ItemInformation
{
    Weapon weapon01 = new Weapon(1, ItemType.Weapon, "HATTER", 1, 5, 1, 0, 0);
    Weapon weapon02 = new Weapon(2, ItemType.Weapon, "NARVAL", 3, 25, 0.3f, 0, 0);
    Weapon weapon03 = new Weapon(3, ItemType.Weapon, "GASMAN", 2, 3, 2.5f, 0, 0);

    Vehicle vehicle101 = new Vehicle(101, ItemType.Vehicle, "Old Red", 200, 0, 5, 1.2f, 0.1f, 0.1f);
    Vehicle vehicle102 = new Vehicle(102, ItemType.Vehicle, "Old Whtie", 200, 5, 7, 1, 0.15f, 0.2f);
    Vehicle Vehicle103 = new Vehicle(103, ItemType.Vehicle, "Metal Purple", 300, 10, 10, 2, 0.3f, 0.3f);

    Item material201 = new Item(201, ItemType.Material, "철광석");
    Item material202 = new Item(202, ItemType.Material, "화약가루");
    Item material203 = new Item(203, ItemType.Material, "물");
    Item material204 = new Item(204, ItemType.Material, "옥수수통조림");
    Item material205 = new Item(205, ItemType.Material, "고등어통조림");
   
    public List<Weapon> weapons = new List<Weapon>();
    public List<Vehicle> vehicles = new List<Vehicle>();
    public List<Item> materials = new List<Item>();

    public Weapon GetWeapon(int id)
    {
        Weapon weapon = null;
        foreach(Weapon w in weapons)
        {
            if(id == w.GetId())
            {
                weapon = w;
            }
        }
        return weapon;
    }

    public Vehicle GetVehicle(int id)
    {
        Vehicle vehicle = null;
        foreach(Vehicle v in vehicles)
        {
            if(id == v.GetId())
            {
                vehicle = v;
            }
        }
        return vehicle;
    }

    public ItemType GetItemType(int id)
    {
        if (id > 0 && id <= 100)
            return ItemType.Weapon;
        else if (id > 100 && id <= 200)
            return ItemType.Vehicle;

        return ItemType.Material;
    }
    
    // 아이템을 List에 저장
    public void initLists()
    { 
        weapons.Add(weapon01);
        weapons.Add(weapon02);
        weapons.Add(weapon03);

        vehicles.Add(vehicle101);
        vehicles.Add(vehicle102);
        vehicles.Add(Vehicle103);

        // item management
        materials.Add(weapon01);
        materials.Add(weapon02);
        materials.Add(weapon03);
        materials.Add(vehicle101);
        materials.Add(vehicle102);
        materials.Add(Vehicle103);
        materials.Add(material201);
        materials.Add(material202);
        materials.Add(material203);
        materials.Add(material204);
        materials.Add(material205);
    }
}
