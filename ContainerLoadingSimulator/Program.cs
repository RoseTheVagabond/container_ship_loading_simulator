using ContainerLoadingSimulator.Containers;
namespace ContainerLoadingSimulator;

class Program
{
    static void Main(string[] args)
    { 
        Console.WriteLine("Creating different types of containers...");
        GasContainer gas1 = new GasContainer(300, 400, 2500, 25000, 100);
        GasContainer gas2 = new GasContainer(320, 450, 2500, 30000, 120);
        
        LiquidContainer liquid1 = new LiquidContainer(300, 400, 2500, 40000, false);
        LiquidContainer liquid2 = new LiquidContainer(300, 400, 2500, 25000, true);
        
        RefrigeratedContainer refrigerated1 = new RefrigeratedContainer(400, 500, 2500, 25000, 18);
        RefrigeratedContainer refrigerated2 = new RefrigeratedContainer(400, 500, 2500, 25000, -20);
        
        List<Container> containers = new List<Container>();
        containers.Add(gas1);
        containers.Add(gas2);
        containers.Add(liquid1);
        containers.Add(liquid2);
        containers.Add(refrigerated1);
        containers.Add(refrigerated2);

        // Print information about all containers
        Console.WriteLine("\nList of all containers:");
        foreach (var container in containers)
        {
            Console.WriteLine(container.ToString());
        }

        // Create ships
        Console.WriteLine("\nCreating container ships...");
        Ship ship1 = new Ship(30, 10, 16);
        Ship ship2 = new Ship(25, 8, 800);
        
        Console.WriteLine($"Ship {ship1.ShipNumber} created");
        Console.WriteLine($"Ship {ship2.ShipNumber} created");

        // Test loading cargo into containers
        Console.WriteLine("\nTesting container loading functionality...");
        
        Console.WriteLine("\n1. Loading gas container:");
        try
        {
            gas1.Load(10000);
            Console.WriteLine($"Successfully loaded 10000kg into {gas1.GetSerialNumber()}");
        }
        catch (OverfillException ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
        
        Console.WriteLine("\n2. Loading regular liquid container with hazardous cargo:");
        liquid1.Load(15000, true);
        Console.WriteLine($"Container status: {liquid1}");
        
        Console.WriteLine("\n3. Loading hazardous liquid container with hazardous cargo:");
        liquid2.Load(10000, true);
        Console.WriteLine($"Container status: {liquid2}");
        
        Console.WriteLine("\n4. Testing overfill of hazardous liquid container:");
        liquid2.Load(15000, true);
        Console.WriteLine($"Container status after attempted overfill: {liquid2}");
        
        Console.WriteLine("\n5. Loading refrigerated container with different products:");
        refrigerated1.Load(5000, "chocolate", 18);
        Console.WriteLine($"Container status: {refrigerated1}");
        
        Console.WriteLine("\n6. Trying to load different product type into same refrigerated container:");
        refrigerated1.Load(2000, "bananas", 13.3);
        Console.WriteLine($"Container status: {refrigerated1}");
        
        Console.WriteLine("\n7. Loading product that requires lower temperature than container can provide:");
        refrigerated2.Load(3000, "frozen pizza", -30);
        Console.WriteLine($"Container status: {refrigerated2}");
        
        Console.WriteLine("\n7. Loading correct product to the second refrigerated container:");
        refrigerated2.Load(3000, "frozen pizza", -20);
        Console.WriteLine($"Container status: {refrigerated2}");

        // Test loading containers onto ships
        Console.WriteLine("\nTesting ship loading functionality...");
        
        ship1.Load(gas1);
        ship1.Load(liquid1);
        ship1.Load(refrigerated1);
        
        ship2.Load(gas2);
        ship2.Load(liquid2);
        ship2.Load(refrigerated2);
        
        ship1.PrintShipInfo();
        ship2.PrintShipInfo();
        
        // Test removing container from ship
        Console.WriteLine("\nTesting container removal functionality...");
        string containerToRemove = gas1.GetSerialNumber();
        Console.WriteLine($"Removing container {containerToRemove} from Ship 1:");
        ship1.RemoveContainer(containerToRemove);

        ship1.PrintShipInfo();
        
        // Test replacing container on ship
        Console.WriteLine("\nTesting container replacement functionality (with a container that's on a different ship)...");
        string containerToReplace = liquid1.GetSerialNumber();
        Console.WriteLine($"Replacing container {containerToReplace} on Ship 1 with {gas2.GetSerialNumber()}:");
        ship1.ReplaceContainer(containerToReplace, gas2);
        
        Console.WriteLine("\nTesting container replacement functionality (with a container that's available)...");
        Console.WriteLine($"Replacing container {containerToReplace} on Ship 1 with {gas1.GetSerialNumber()}:");
        ship1.ReplaceContainer(containerToReplace, gas1);

        
        Console.WriteLine("\nShip 1 Information after replacement:");
        ship1.PrintShipInfo();
        
        // Test transferring container between ships
        Console.WriteLine("\nTesting container transfer functionality...");
        string containerToTransfer = refrigerated2.GetSerialNumber();
        Console.WriteLine($"Transferring container {containerToTransfer} from Ship 2 to Ship 1:");
        ship2.TransferContainer(containerToTransfer, ship1);
        
        Console.WriteLine("\nShip 1 Information after transfer:");
        ship1.PrintShipInfo();
        
        Console.WriteLine("\nShip 2 Information after transfer:");
        ship2.PrintShipInfo();
        
        // Test emptying containers
        Console.WriteLine("\nTesting container emptying functionality...");
        
        Console.WriteLine("\n1. Emptying gas container (should leave 5% of cargo):");
        Console.WriteLine($"Gas container before emptying: {gas1}");
        gas1.Empty();
        Console.WriteLine($"Gas container after emptying: {gas1}");
        
        Console.WriteLine("\n2. Emptying liquid container:");
        Console.WriteLine($"Liquid container before emptying: {liquid1}");
        liquid1.Empty();
        Console.WriteLine($"Liquid container after emptying: {liquid1}");
        
        // Test overfill exception
        Console.WriteLine("\nTesting overfill exception:");
        try
        {
            Console.WriteLine("Attempting to load excessive cargo into gas container:");
            gas1.Load(30000);
        }
        catch (OverfillException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
        
        // Test loading multiple containers at once
        Console.WriteLine("\nTesting loading multiple containers onto a ship at once:");
        Ship ship3 = new Ship(40, 20, 2000);
        Console.WriteLine($"Created Ship {ship3.ShipNumber}");
        
        List<Container> containersToLoad = new List<Container>
        {
            new GasContainer(300, 400, 2500, 25000, 100),
            new LiquidContainer(300, 400, 2500, 40000, false),
            new RefrigeratedContainer(400, 500, 2500, 25000, 18)
        };
        
        Console.WriteLine("Loading list of containers onto Ship 3:");
        foreach (var container in containersToLoad)
        {
            ship3.Load(container);
        }
        
        Console.WriteLine("\nShip 3 Information after bulk loading:");
        ship3.PrintShipInfo();
    }
}