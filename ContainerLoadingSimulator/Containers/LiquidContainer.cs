namespace ContainerLoadingSimulator.Containers;

public class LiquidContainer : Container, IHazardNotifier
{
    private static int _containerCounter = 1;
    private bool isHazardous {get;set;}

    public LiquidContainer(double height, double tareWeight, double depth, double maxPayload, bool isHazardous) :
        base(height, tareWeight, depth, "KON-L-" + _containerCounter++, maxPayload)
    {
        this.isHazardous = isHazardous;
    }

    public string Notify()
    {
        return "Hazardous situation occurred in liquid container nr: " + serialNumber;
    }

    public override void Load(double productMass)
    {
        Load(productMass, false);
    }

    public void Load(double productMass, bool liquidHazardous)
    {
        if (liquidHazardous && !isHazardous)
        {
            Console.WriteLine(Notify());
            Console.WriteLine("This container is not suited for transporting hazardous cargo");
        }
        else
        {
            double maxAllowedCapacity = isHazardous ? maxPayload * 0.5 : maxPayload * 0.9;
        
            if (cargoMass + productMass > maxAllowedCapacity)
            {
                Console.WriteLine(Notify());
                Console.WriteLine(isHazardous 
                    ? "Loading aborted - hazardous cargo can occupy maximally 50% of the container capacity" 
                    : "Loading aborted - regular liquid cargo can occupy maximally 90% of the container capacity");
            }
            else
            {
                cargoMass += productMass;
            }
        }
    }
    
    public override string ToString()
    {
        return base.ToString() + $", Hazardous={isHazardous}";
    }
}