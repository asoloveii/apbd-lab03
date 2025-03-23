using lab03.Exceptions;

namespace lab03.Containers;

public class RefrigeratedContainer : Container
{
    private double Temperature { get; }
    private string ProductType { get; }

    private static Dictionary<string, double> products = new Dictionary<string, double>
    {
        ["Bananas"] = 13.3,
        ["Chocolate"] = 18,
        ["Fish"] = 2,
        ["Meat"] = -15,
        ["Ice Cream"] = -18,
        ["Frozen Pizza"] = -30,
        ["Cheese"] = 7.2,
        ["Sausages"] = 5,
        ["Butter"] = 20.5,
        ["Eggs"] = 19,
    };

    public RefrigeratedContainer(double temperature, string productType, int maxPayload) : base('R', maxPayload)
    {
        ValidateParams(temperature, productType);
        Temperature = temperature;
        ProductType = productType;
    }

    private void ValidateParams(double temperature, string productType)
    {
        if (!products.ContainsKey(productType)) throw new InvalidOperationException("Invalid product type");
        if (temperature < products[productType])
            throw new InvalidOperationException("Invalid temperature for this product");
    }

    public override void EmptyCargo()
    {
        CargoWeight = 0;
    }

    public override void LoadCargo(int payload)
    {
        if (CargoWeight + payload > MaxPayload) throw new OverfillException("[Exception] Refrigerated Container overflow");
        CargoWeight += payload;
    }
}