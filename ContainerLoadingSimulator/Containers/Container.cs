namespace ContainerLoadingSimulator;

public abstract class Container
{
    protected double cargoMass { get; set; } = 0;
    protected double height { get; set; }
    protected double tareWeight { get; set; }
    protected double depth { get; set; }
    protected string serialNumber { get; set; }
    protected double maxPayload { get; set; }

    public Container(double height, double tareWeight, double depth,
        string serialNumber, double maxPayload)
    {
        this.height = height;
        this.tareWeight = tareWeight;
        this.depth = depth;
        this.serialNumber = serialNumber;
        this.maxPayload = maxPayload;
    }

    public virtual void Empty()
    {
        this.cargoMass = 0;
    }

    public virtual void Load(double productMass)
    {
        if (this.cargoMass + productMass > this.maxPayload)
        {
            throw new OverfillException();
        }
        else
        {
            this.cargoMass += productMass;
        }
    }

    public double GetTotalWeight()
    {
        return this.cargoMass + this.tareWeight;
    }

    public string GetSerialNumber()
    {
        return this.serialNumber;
    }
    
    public override string ToString()
        {
            return $"Container {serialNumber}: Height={height}cm, Depth={depth}cm, Tare Weight={tareWeight}kg, " +
                   $"Cargo Mass={cargoMass}kg, Max Payload={maxPayload}kg, Total Weight={GetTotalWeight()}kg";
        }
}