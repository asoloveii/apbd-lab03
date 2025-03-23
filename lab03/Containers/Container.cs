namespace lab03.Containers;
using System;

public abstract class Container
{
    private int Mass { get; set; } // the mass of the cargo, in kg
    private int Height { get; set; } // in cm
    private int TareWeight { get; set; } // the weight of the container itself, in kg
    private int CargoWeight { get; set; } // the weight of the cargo itself, in kg
    private int Depth { get; set; } // in cm
    private string SerialNumber { get; }
    private int MaxPayload { get; set; } // max payload of a container, in kg
    
    private static readonly object _lock = new object();
    private static int _nextId;

    protected Container(char containerType, int maxPayload)
    {
        if (!char.IsLetter(containerType)) throw new ArgumentException("Invalid container type");
        int uniqueId;
        // ensuring that one thread is a piece of code at one time
        lock (_lock)
        {
            uniqueId = _nextId++;
        }
        // setting initial fields
        SerialNumber = $"KON-{containerType}-{uniqueId}";
        MaxPayload = maxPayload;
        CargoWeight = 0;
    }
    
    public abstract void EmptyCargo();
    public abstract void LoadCargo(int payload);
}