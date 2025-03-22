using ContainerLoadingSimulator;
using ContainerLoadingSimulator.Containers;

class Program
{
    static void Main(string[] args)
    {
        GasContainer gas1 = new GasContainer(300, 400, 2500, 25000, 100);
        LiquidContainer liquid1 = new LiquidContainer(300, 400, 2500, 40000, false);
        RefrigeratedContainer refrigerated1 = new RefrigeratedContainer(400, 500, 2500, 25000, 18);
        
        LiquidContainer liquid2 = new LiquidContainer(300, 400, 2500, 25000, true);
        
        List<Container> containers = new List<Container>();
        containers.Add(gas1);
        containers.Add(liquid1);
        containers.Add(refrigerated1);

        for (int i = 0; i < containers.Count; i++)
        {
            Console.WriteLine(containers[i].ToString());
        }
        
        gas1.Load(1000);
        liquid1.Load(1000, true);
        
        liquid2.Load(1000, true);
        Console.WriteLine(liquid2.ToString());

        Ship ship1 = new Ship(30, 10, 1000);
        ship1.Load(liquid2);
        ship1.PrintShipInfo();
    }
}