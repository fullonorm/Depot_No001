public class Equipment 
{
    private int weaponID;
    private int vehicleID;
    
    public Equipment() { }

    public Equipment(int w, int v)
    {
        weaponID = w;
        vehicleID = v;
    }

    public int GetWeapon()
    {
        return weaponID;
    }

    public int GetVehicle()
    {
        return vehicleID;
    }

    public void SetWeapon(int w)
    {
        weaponID = w;
    }

    public void SetVehicle(int v)
    {
        vehicleID = v;
    }

}
