namespace ContainerLoadingSimulator.Containers;

public class GasContainer : Container, IHazardNotifier
{
    private static int _containerCounter = 1;
    private int Pressure { get; set; }

    public GasContainer(double height, double tareWeight, double depth, double maxPayload, int pressure) :
        base(height, tareWeight, depth, "KON-G-" + _containerCounter++, maxPayload)
    {
        this.Pressure = pressure;
    }

    public string Notify()
    {
        return "Hazardous situation occurred in gas container nr: " + SerialNumber;
    }
    
    public override void Load(double productMass)
    {
        if (CargoMass + productMass > MaxPayload)
        {
            Console.WriteLine(Notify());
            throw new OverfillException("Cannot load gas: maximum payload exceeded");
        }
        CargoMass += productMass;
    }

    public override void Empty()
    {
        CargoMass *= 0.05;
    }
    
    public override string ToString()
    {
        return base.ToString() + $", Pressure={Pressure} atm";
    }
    
}