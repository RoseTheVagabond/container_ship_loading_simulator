namespace ContainerLoadingSimulator;

public class LiquidContainer : Container, IHazardNotifier
{
    private static int ContainerCounter = 1;
    public LiquidContainer(double height, double tareWeight, double depth, double maxPayload) : 
        base(height, tareWeight, depth, "KON-L-" + ContainerCounter++, maxPayload) {}

    public string Notify()
    {
        return "Hazardous situation occurred in liquid container nr: " + serialNumber;
    }

    public void Load(Liquid gas)
    {
        if (gas.hazardous && cargoMass + gas.mass > maxPayload / 2)
        {
            Console.WriteLine(Notify());
            throw new OverfillException("Loading aborted - hazardous cargo can occupy maximally 50% of the container capacity");
        } 
        else if (!gas.hazardous && cargoMass + gas.mass > maxPayload * 0.9)
        {
            throw new OverfillException("Loading aborted - regular liquid cargo can occupy maximally 90% of the container capacity");
        }
        else
        {
            cargoMass += gas.mass;
        }
    }
}