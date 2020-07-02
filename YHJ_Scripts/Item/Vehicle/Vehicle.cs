public class Vehicle : Item
{
    private float hp;
    private float def;
    private float velocity;
    private float acceleration;
    private float torque;
    private float breaking;

    public Vehicle(int id, ItemType type, string name, float hp, float def,
        float velocity, float acceleration, float torque, float breaking) : base(id, type, name)
    {
        this.hp = hp;
        this.def = def;
        this.velocity = velocity;
        this.acceleration = acceleration;
        this.torque = torque;
        this.breaking = breaking;
    }

    public float GetHp()
    {
        return hp;
    }

    public float GetDef()
    {
        return def;
    }

    public float GetVelocity()
    {
        return velocity;
    }

    public float GetAcceleration()
    {
        return acceleration;
    }

    public float GetTorque()
    {
        return torque;
    }

    public float Getbreaking()
    {
        return breaking;
    }
}
