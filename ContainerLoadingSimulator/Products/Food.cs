namespace ContainerLoadingSimulator;

public class Food : Product
{
    public double Temperature { get; set; }
    public Food(double mass, double temperature) : base(mass)
    {
        this.Temperature = temperature;
    }
}