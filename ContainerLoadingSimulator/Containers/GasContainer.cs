namespace ContainerLoadingSimulator;

public class GasContainer : Container, IHazardNotifier
{
    private static int ContainerCounter = 1;
    private int Pressure { get; set; }

    public GasContainer(double height, double tareWeight, double depth, double maxPayload, int pressure) :
        base(height, tareWeight, depth, "KON-G-" + ContainerCounter++, maxPayload)
    {
        this.Pressure = pressure;
    }

    public string Notify()
    {
        return "Hazardous situation occurred in gas container nr: " + serialNumber;
    }
    
    public override void Load(double productMass)
    {
        if (cargoMass + productMass > maxPayload)
        {
            Console.WriteLine(Notify());
            throw new OverfillException("Cannot load gas: maximum payload exceeded");
        }
        cargoMass += productMass;
    }

    public override void Empty()
    {
        cargoMass *= 0.05;
    }
    
    public override string ToString()
    {
        return base.ToString() + $", Pressure={Pressure} atm";
    }
    
}