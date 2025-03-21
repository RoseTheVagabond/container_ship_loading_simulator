namespace ContainerLoadingSimulator;

public class Ship
{
    public static int ShipCounter = 1;
    
    public int ShipNumber;
    public List<Container> Containers { get; set; } = new List<Container>();
    public double MaximumSpeed { get; set; }
    public int MaximumContainers { get; set; }
    public double MaximumContainerWeightTons { get; set; }
    public double ContainerWeightTons { get; set; } = 0;

    public Ship(double maximumSpeed, int maxContainers, double maxContainerWeightTons)
    {
        this.ShipNumber = ShipCounter++;
        this.MaximumSpeed = maximumSpeed;
        this.MaximumContainers = maxContainers;
        this.MaximumContainerWeightTons = maxContainerWeightTons;
    }

    public bool Load(Container container)
    {
        if (ContainerWeightTons + (container.GetTotalWeight() * 0.001) > MaximumContainerWeightTons)
        {
            Console.WriteLine("Can't load another container with a total weight of " + container.GetTotalWeight() + ".");
            return false;
        } 
        if (Containers.Count >= MaximumContainers)
        {
            Console.WriteLine("Can't load another container, because the maximum number of containers has been reached.");
            return false;
        }
        Containers.Add(container);
        ContainerWeightTons += container.GetTotalWeight() * 0.001;
        return true;
    }
    
    public bool RemoveContainer(string serialNumber)
    {
        Container containerToRemove = Containers.Find(c => c.GetSerialNumber() == serialNumber);
        if (containerToRemove != null)
        {
            Containers.Remove(containerToRemove);
            ContainerWeightTons -= containerToRemove.GetTotalWeight() * 0.001;
            Console.WriteLine($"Container {serialNumber} removed from ship {ShipNumber}");
            return true;
        }
        Console.WriteLine($"Container {serialNumber} not found on ship {ShipNumber}");
        return false;
    }
    
    public void ReplaceContainer(string serialNumberOld, Container newContainer)
    {
        RemoveContainer(serialNumberOld);
        Load(newContainer);
    }
    
    public bool TransferContainer(string serialNumber, Ship destinationShip)
    {
        Container containerToTransfer = Containers.Find(c => c.GetSerialNumber() == serialNumber);
        if (containerToTransfer != null)
        {
            if (destinationShip.Load(containerToTransfer))
            {
                Containers.Remove(containerToTransfer);
                Console.WriteLine($"Container {serialNumber} transferred from ship {ShipNumber} to ship {destinationShip.ShipNumber}");
                return true;
            }
            Console.WriteLine($"Failed to transfer container {serialNumber} to ship {destinationShip.ShipNumber}");
            return false;
        }
        Console.WriteLine($"Container {serialNumber} not found on ship {ShipNumber}");
        return false;
    }
    
    public void PrintShipInfo()
    {
        Console.WriteLine($"Max Speed: {MaximumSpeed} knots");
        Console.WriteLine($"Number of Containers: {Containers.Count}");
        Console.WriteLine($"Max Container Capacity: {MaximumContainers}");
        Console.WriteLine($"Total Weight of containers in tons: {ContainerWeightTons} tons");
        Console.WriteLine($"Max allowed Container Weight: {MaximumContainerWeightTons}");
        
        if (Containers.Count > 0)
        {
            Console.WriteLine("Containers on board:");
            foreach (var container in Containers)
            {
                Console.WriteLine($"  - {container.ToString()}");
            }
        }
        else
        {
            Console.WriteLine("No containers on board.");
        }
    }
    
}