namespace ContainerLoadingSimulator;

public class Liquid : Product
{
    public bool hazardous { set; get; }
    public Liquid(double mass, bool hazardous) : base(mass)
    {
        this.hazardous = hazardous;
    }
}