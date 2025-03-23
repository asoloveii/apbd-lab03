using lab03.Containers;
using lab03.Exceptions;

namespace lab03.Transport;

public class Ship
{
    private List<Container> Containers { get; }
    private int MaxSpeed { get; } // in knots
    private int MaxWeight { get; } // in tons
    private int MaxCapacity { get; } // max number of container
    private int Id { get; }
    private static int _counter = 0;

    public Ship(int maxSpeed, int maxWeight, int maxCapacity)
    {
        MaxSpeed = maxSpeed;
        MaxWeight = maxWeight;
        MaxCapacity = maxCapacity;
        Containers = new List<Container>();
        Id = _counter++;
    }

    public void LoadContainer(Container container)
    {
        // check for the number of containers
        if (Containers.Count == MaxCapacity) 
            throw new OverfillException("[Exception] Cannot add more containers than max capacity");

        int totalWeight = Containers.Sum(c => c.CargoWeight + c.TareWeight) 
                          + container.CargoWeight + container.TareWeight;
        if (totalWeight > MaxWeight*1000)
            throw new OverfillException("[Exception] The total weight of containers exceeds max weight");
        
        Containers.Add(container);
    }

    public void LoadContainers(List<Container> containers)
    {
        foreach (var container in containers)
        {
            LoadContainer(container);
        }
    }

    public void RemoveContainer(Container container)
    {
        Containers.Remove(container);
    }
    
    public void PrintShipInfo()
    {
        Console.WriteLine($"Ship Max Speed: {MaxSpeed} knots, Max Containers: {MaxCapacity}, Max Weight: {MaxWeight} tons");
        Console.WriteLine("Containers on board:");
        foreach (var container in Containers)
        {
            Console.WriteLine($"{container.SerialNumber}");
        }
    }
}