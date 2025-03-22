using ContainerLoadingSimulator.Containers;
namespace ContainerLoadingSimulator;

public class Ship
{
    private static int _shipCounter = 1;
    
    public int ShipNumber;
    private List<Container> Containers { get; set; } = new List<Container>();
    private double MaximumSpeed { get; set; }
    private int MaximumContainers { get; set; }
    private double MaximumContainerWeightTons { get; set; }
    private double ContainerWeightTons { get; set; }
    
    private static Dictionary<string, Ship> _containerShipMap = new Dictionary<string, Ship>();

    public Ship(double maximumSpeed, int maxContainers, double maxContainerWeightTons)
    {
        this.ShipNumber = _shipCounter++;
        this.MaximumSpeed = maximumSpeed;
        this.MaximumContainers = maxContainers;
        this.MaximumContainerWeightTons = maxContainerWeightTons;
    }

    public bool Load(Container container)
    {
        string serialNumber = container.GetSerialNumber();
        
        if (_containerShipMap.ContainsKey(serialNumber))
        {
            Ship currentShip = _containerShipMap[serialNumber];
            if (currentShip != this)
            {
                Console.WriteLine($"Can't load container {serialNumber}. It's already on Ship {currentShip.ShipNumber}.");
                return false;
            }
        }
        
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
        _containerShipMap[serialNumber] = this;
        
        Console.WriteLine($"Container {serialNumber} loaded onto Ship {ShipNumber}");
        return true;
    }
    
    public bool RemoveContainer(string serialNumber)
    {
        Container containerToRemove = Containers.Find(c => c.GetSerialNumber() == serialNumber);
        if (containerToRemove != null)
        {
            Containers.Remove(containerToRemove);
            ContainerWeightTons -= containerToRemove.GetTotalWeight() * 0.001;
            
            if (_containerShipMap.ContainsKey(serialNumber))
            {
                _containerShipMap.Remove(serialNumber);
            }
            
            Console.WriteLine($"Container {serialNumber} removed from ship {ShipNumber}");
            return true;
        }
        Console.WriteLine($"Container {serialNumber} not found on ship {ShipNumber}");
        return false;
    }
    
    public void ReplaceContainer(string serialNumberOld, Container newContainer)
    {
        string newSerialNumber = newContainer.GetSerialNumber();
        if (_containerShipMap.ContainsKey(newSerialNumber) && _containerShipMap[newSerialNumber] != this)
        {
            Ship currentShip = _containerShipMap[newSerialNumber];
            Console.WriteLine($"Can't replace with container {newSerialNumber}. It's already on Ship {currentShip.ShipNumber}.");
            return;
        }
        
        if (RemoveContainer(serialNumberOld))
        {
            Load(newContainer);
        }
    }
    
    public bool TransferContainer(string serialNumber, Ship destinationShip)
    {
        Container containerToTransfer = Containers.Find(c => c.GetSerialNumber() == serialNumber);
        if (containerToTransfer != null)
        {
            Containers.Remove(containerToTransfer);
            ContainerWeightTons -= containerToTransfer.GetTotalWeight() * 0.001;
            if (_containerShipMap.ContainsKey(serialNumber))
            {
                _containerShipMap.Remove(serialNumber);
            }
            
            if (destinationShip.Load(containerToTransfer))
            {
                Console.WriteLine($"Container {serialNumber} transferred from ship {ShipNumber} to ship {destinationShip.ShipNumber}");
                return true;
            }
            else
            {
                Containers.Add(containerToTransfer);
                ContainerWeightTons += containerToTransfer.GetTotalWeight() * 0.001;
                _containerShipMap[serialNumber] = this;
                
                Console.WriteLine($"Failed to transfer container {serialNumber} to ship {destinationShip.ShipNumber}");
                return false;
            }
        }
        Console.WriteLine($"Container {serialNumber} not found on ship {ShipNumber}");
        return false;
    }
    
    public void PrintShipInfo()
    {
        Console.WriteLine($"Ship {ShipNumber} Information:");
        Console.WriteLine($"Max Speed: {MaximumSpeed} knots");
        Console.WriteLine($"Number of Containers: {Containers.Count}");
        Console.WriteLine($"Max Container Capacity: {MaximumContainers}");
        Console.WriteLine($"Total Weight of containers in tons: {ContainerWeightTons:F3} tons");
        Console.WriteLine($"Max allowed Container Weight: {MaximumContainerWeightTons} tons");
        
        if (Containers.Count > 0)
        {
            Console.WriteLine("Containers on board:");
            foreach (var container in Containers)
            {
                Console.WriteLine($"  - {container}");
            }
        }
        else
        {
            Console.WriteLine("No containers on board.");
        }
    }
}