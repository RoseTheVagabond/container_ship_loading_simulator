namespace ContainerLoadingSimulator;

public class RefrigeratedContainer : Container
{
    private static int ContainerCounter = 1;
    protected Product StoredProduct { get; set; }
    protected double Temperature { get; set; }
    public RefrigeratedContainer(double height, double tareWeight, double depth, double maxPayload, double temperature) 
        : base(height, tareWeight, depth, "KON-C-" + ContainerCounter++, maxPayload)
    {
        this.Temperature = temperature;
    }
    
    public void Load(Food food)
    {
        if (this.cargoMass + food.mass > maxPayload)
        {
            throw new OverfillException();
        }
        else
        {
            
        }
    }
}