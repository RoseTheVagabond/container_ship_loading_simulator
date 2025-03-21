namespace ContainerLoadingSimulator.Containers;

public class RefrigeratedContainer : Container
{
    private static int _containerCounter = 1;
    protected double Temperature { get; set; }
    protected string ProductType { get; set; }
    public RefrigeratedContainer(double height, double tareWeight, double depth, double maxPayload, double temperature) 
        : base(height, tareWeight, depth, "KON-C-" + _containerCounter++, maxPayload)
    {
        this.Temperature = temperature;
    }
    
    public override void Load(double productMass)
    {
        throw new InvalidOperationException("For refrigerated containers, you must specify product type and temperature");
    }
    
    public void Load(double productMass, string productType, double requiredTemperature)
    {
        if (cargoMass > 0 && this.ProductType != productType)
        {
            throw new InvalidOperationException($"Cannot mix product types. Container already contains {this.ProductType}");
        }
        
        if (Temperature < requiredTemperature)
        {
            Console.WriteLine($"Container {serialNumber} cannot store {productType}, because temperature is too low");
            Console.WriteLine($"Temperature in container: {this.Temperature}, temperature required: {requiredTemperature}");
            throw new InvalidOperationException("Temperature requirements not met");
        }
        
        if (cargoMass + productMass > maxPayload)
        {
            throw new OverfillException($"Cannot load {productType}: maximum payload exceeded");
        }
        cargoMass += productMass;
        this.ProductType = productType;
    }
    
    public override string ToString()
    {
        return base.ToString() + $", Temperature={Temperature}°C, Product Type={ProductType ?? "Empty"}";
    }
}