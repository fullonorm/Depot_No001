public class Weapon : Item
{
    private float range;
    private float damage;
    private float rof;
    private float reload;
    private float ammo;

    public Weapon(int id, ItemType type, string name, float range,
        float damage, float rof, float reload, float ammo) : base(id, type, name)
    {
        this.range = range;
        this.damage = damage;
        this.rof = rof;
        this.reload = reload;
        this.ammo = ammo;
    }

    public float GetRange()
    {
        return range;
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetROF()
    {
        return rof;
    }

    public float GetReload()
    {
        return reload;
    }

    public float GetAmmo()
    {
        return ammo;
    }
}

