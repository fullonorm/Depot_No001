public class Item 
{
    private int id;
    private ItemType type;
    private string name;

    public Item(int id, ItemType type, string name)
    {
        this.id = id;
        this.type = type;
        this.name = name;
    }

    public int GetId()
    {
        return id;
    }

    public ItemType GetItemType()
    {
        return type;
    }

    public string GetName()
    {
        return name;
    }
}

public enum ItemType
{
    Vehicle, Weapon, Material 
}
