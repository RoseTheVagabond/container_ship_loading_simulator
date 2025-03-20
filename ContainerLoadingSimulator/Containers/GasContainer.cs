namespace ContainerLoadingSimulator;

public class GasContainer : Container, IHazardNotifier
{
    private static int ContainerCounter = 1;
    private int Pressure { get; set; }
    public GasContainer(double height, double tareWeight, double depth, double maxPayload) : 
        base(height, tareWeight, depth, "KON-G-" + ContainerCounter++, maxPayload) {}

    public string Notify()
    {
        return "Hazardous situation occurred in gas container nr: " + serialNumber;
    }

    public void Load(Gas gas)
    {
        base.Load(gas);
    }

    public void Empty()
    {
        cargoMass *= 0.05;
    }
    
}