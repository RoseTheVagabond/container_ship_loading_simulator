namespace ContainerLoadingSimulator.Containers;

public abstract class Container
{
    protected double CargoMass { get; set; }
    private double Height { get; set; }
    private double TareWeight { get; set; }
    private double Depth { get; set; }
    protected string SerialNumber { get; set; }
    protected double MaxPayload { get; set; }

    public Container(double height, double tareWeight, double depth,
        string serialNumber, double maxPayload)
    {
        this.Height = height;
        this.TareWeight = tareWeight;
        this.Depth = depth;
        this.SerialNumber = serialNumber;
        this.MaxPayload = maxPayload;
    }

    public virtual void Empty()
    {
        this.CargoMass = 0;
    }

    public virtual void Load(double productMass)
    {
        if (this.CargoMass + productMass > this.MaxPayload)
        {
            throw new OverfillException();
        }
        else
        {
            this.CargoMass += productMass;
        }
    }

    public double GetTotalWeight()
    {
        return this.CargoMass + this.TareWeight;
    }

    public string GetSerialNumber()
    {
        return this.SerialNumber;
    }
    
    public override string ToString()
        {
            return $"Container {SerialNumber}: Height={Height}cm, Depth={Depth}cm, Tare Weight={TareWeight}kg, " +
                   $"Cargo Mass={CargoMass}kg, Max Payload={MaxPayload}kg, Total Weight={GetTotalWeight()}kg";
        }
}