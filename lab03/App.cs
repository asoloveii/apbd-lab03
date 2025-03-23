namespace lab03;

using System;
using System.Collections.Generic;
using lab03.Containers;
using lab03.Exceptions;
using lab03.Transport;

class App
{
    static List<Ship> ships = new List<Ship>();
    static List<Container> containers = new List<Container>();

    public static void Run()
    {
        while (true)
        {
            Console.Clear();
            PrintStatus();
            Console.WriteLine("Possible actions:");
            Console.WriteLine("1. Add a container ship");
            Console.WriteLine("2. Remove a container ship");
            Console.WriteLine("3. Add a container");
            Console.WriteLine("4. Load container onto ship");
            Console.WriteLine("5. Remove a container from ship");
            Console.WriteLine("6. Unload a container");
            Console.WriteLine("7. Transfer container between ships");
            Console.WriteLine("8. Print container information");
            Console.WriteLine("9. Print ship information");
            Console.WriteLine("0. Exit");
            Console.Write("Select an option: ");
            
            switch (Console.ReadLine())
            {
                case "1": AddShip(); break;
                case "2": RemoveShip(); break;
                case "3": AddContainer(); break;
                case "4": LoadContainerOntoShip(); break;
                case "5": RemoveContainerFromShip(); break;
                case "6": UnloadContainer(); break;
                case "7": TransferContainerBetweenShips(); break;
                case "8": PrintContainerInfo(); break;
                case "9": PrintShipInfo(); break;
                case "0": return;
                default: Console.WriteLine("Invalid option. Press any key to continue..."); Console.ReadKey(); break;
            }
        }
    }

    static void PrintStatus()
    {
        Console.WriteLine("List of container ships: " + (ships.Count == 0 ? "None" : string.Join(", ", ships)));
        Console.WriteLine("List of containers: " + (containers.Count == 0 ? "None" : string.Join(", ", containers)));
        Console.WriteLine();
    }

    static void AddShip()
    {
        Console.Write("Enter ship speed (knots): ");
        int speed = int.Parse(Console.ReadLine());
        Console.Write("Enter max container number: ");
        int maxContainers = int.Parse(Console.ReadLine());
        Console.Write("Enter max weight (tons): ");
        int maxWeight = int.Parse(Console.ReadLine());
        ships.Add(new Ship(speed, maxWeight, maxContainers));
    }

    static void RemoveShip()
    {
        Console.Write("Enter ship index to remove: ");
        int index = int.Parse(Console.ReadLine());
        if (index >= 0 && index < ships.Count)
            ships.RemoveAt(index);
    }

    static void AddContainer()
    {
        Console.Write("Select container type (G/L/R): ");
        char type = Console.ReadLine()[0];
        Console.Write("Enter max payload: ");
        int maxPayload = int.Parse(Console.ReadLine());

        switch (type)
        {
            case 'G':
                Console.Write("Enter pressure: ");
                int pressure = int.Parse(Console.ReadLine());
                Console.Write("Is hazardous? (y/n): ");
                bool isHazardous = Console.ReadLine().ToLower() == "y";
                containers.Add(new GasContainer(maxPayload, pressure, isHazardous));
                break;
            case 'L':
                Console.Write("Is hazardous? (y/n): ");
                bool isHaz = Console.ReadLine().ToLower() == "y";
                containers.Add(new LiquidContainer(maxPayload, isHaz));
                break;
            case 'R':
                Console.Write("Enter temperature: ");
                double temp = double.Parse(Console.ReadLine());
                Console.Write("Enter product type: ");
                string product = Console.ReadLine();
                containers.Add(new RefrigeratedContainer(temp, product, maxPayload));
                break;
        }
    }

    static void LoadContainerOntoShip()
    {
        Console.Write("Enter ship index: ");
        int shipIndex = int.Parse(Console.ReadLine());
        Console.Write("Enter container SerialNumber: ");
        string serialNumber = Console.ReadLine();
        
        var container = containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (shipIndex >= 0 && shipIndex < ships.Count && container != null)
        {
            try
            {
                ships[shipIndex].LoadContainer(container);
            }
            catch (OverfillException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }
    }

    static void RemoveContainerFromShip()
    {
        Console.Write("Enter ship index: ");
        int shipIndex = int.Parse(Console.ReadLine());
        Console.Write("Enter container SerialNumber: ");
        string serialNumber = Console.ReadLine();
        
        var container = containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (shipIndex >= 0 && shipIndex < ships.Count && container != null)
        {
            ships[shipIndex].RemoveContainer(container);
        }
    }

    static void UnloadContainer()
    {
        Console.Write("Enter container SerialNumber: ");
        string serialNumber = Console.ReadLine();
        var container = containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container != null)
            container.EmptyCargo();
    }

    static void TransferContainerBetweenShips()
    {
        Console.Write("Enter source ship index: ");
        int srcIndex = int.Parse(Console.ReadLine());
        Console.Write("Enter destination ship index: ");
        int destIndex = int.Parse(Console.ReadLine());
        Console.Write("Enter container SerialNumber: ");
        string serialNumber = Console.ReadLine();
        
        var container = containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (srcIndex >= 0 && srcIndex < ships.Count && destIndex >= 0 && destIndex < ships.Count && container != null)
        {
            ships[srcIndex].RemoveContainer(container);
            ships[destIndex].LoadContainer(container);
        }
    }

    static void PrintContainerInfo()
    {
        Console.Write("Enter container SerialNumber: ");
        string serialNumber = Console.ReadLine();
        var container = containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container != null)
            Console.WriteLine(container.SerialNumber);
    }

    static void PrintShipInfo()
    {
        Console.Write("Enter ship index: ");
        int index = int.Parse(Console.ReadLine());
        if (index >= 0 && index < ships.Count)
            ships[index].PrintShipInfo();
    }
}
