namespace ContainerLoadingSimulator.Containers;

public class LiquidContainer : Container, IHazardNotifier
{
    private static int _containerCounter = 1;
    private bool isHazardous = false;
    public LiquidContainer(double height, double tareWeight, double depth, double maxPayload) : 
        base(height, tareWeight, depth, "KON-L-" + _containerCounter++, maxPayload) {}

    public string Notify()
    {
        return "Hazardous situation occurred in liquid container nr: " + serialNumber;
    }

    public override void Load(double productMass)
    {
        Load(productMass, false);
    }

    public void Load(double productMass, bool isHazardous)
    {
        this.isHazardous = isHazardous;
        double maxAllowedCapacity = isHazardous ? maxPayload * 0.5 : maxPayload * 0.9;
        
        if (cargoMass + productMass > maxAllowedCapacity)
        {
            Console.WriteLine(Notify());
            throw new OverfillException(isHazardous 
                ? "Loading aborted - hazardous cargo can occupy maximally 50% of the container capacity" 
                : "Loading aborted - regular liquid cargo can occupy maximally 90% of the container capacity");
        }
        cargoMass += productMass;
    }
    
    public override string ToString()
    {
        return base.ToString() + $", Hazardous={isHazardous}";
    }
}