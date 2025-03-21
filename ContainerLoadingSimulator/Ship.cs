namespace ContainerLoadingSimulator;

public class Ship
{
    public List<Container> Containers { get; set; } = new List<Container>();
    public double MaximumSpeed { get; set; }
    public int MaximumContainers { get; set; }
    public double MaximumContainerWeightTons { get; set; }
    public double ContainerWeightTons { get; set; } = 0;

    public Ship(double maximumSpeed, int maxContainers, double maxContainerWeightTons)
    {
        this.MaximumSpeed = maximumSpeed;
        this.MaximumContainers = maxContainers;
        this.MaximumContainerWeightTons = maxContainerWeightTons;
    }

    public void Load(Container container)
    {
        if (ContainerWeightTons + (container.GetTotalWeight() * 0.001) > MaximumContainerWeightTons)
        {
            throw new Exception("Can't load another container with a total weight of " + container.GetTotalWeight() + ".");
        } else if (Containers.Count >= MaximumContainers)
        {
            throw new Exception("Can't load another container, because the maximum number of containers has been reached.");
        }
        Containers.Add(container);
        ContainerWeightTons += container.GetTotalWeight() * 0.001;
    }
    
}