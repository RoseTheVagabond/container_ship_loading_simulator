namespace ContainerLoadingSimulator;

public class Food
{
    public static readonly Dictionary<string, double> StorageTemperatures = new Dictionary<string, double>
    {
        {"bananas", 13.3},
        {"chocolate", 18},
        {"fish", 2},
        {"meat", -15},
        {"ice cream", -18},
        {"frozen pizza", -30},
        {"cheese", 7.2},
        {"sausages", 5},
        {"butter", 20.5},
        {"eggs", 19}
    };

    public static double GetTemperature(string productType)
    {
        if (StorageTemperatures.ContainsKey(productType.ToLower()))
        {
            return StorageTemperatures[productType.ToLower()];
        }
        throw new ArgumentException($"Unknown product type: {productType}");
    }
    
    public static void AddProductType(string productType, double requiredTemperature)
    {
        StorageTemperatures[productType.ToLower()] = requiredTemperature;
    }
}