﻿namespace lab03.Containers;
using System;

public abstract class Container
{
    public int Mass { get; set; } // the mass of the cargo, in kg
    public int Height { get; set; } // in cm
    public int TareWeight { get; set; } // the weight of the container itself, in kg
    public int CargoWeight { get; protected set; } // the weight of the cargo itself, in kg
    public int Depth { get; set; } // in cm
    public string SerialNumber { get; }
    public int MaxPayload { get; protected set; } // max payload of a container, in kg
    
    private static readonly object _lock = new object();
    private static int _nextId = 1;

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

    public override string ToString()
    {
        return $"Container: {SerialNumber} (max payload: {MaxPayload}, cargo weight: {CargoWeight})";
    } 
    
    public abstract void EmptyCargo();
    public abstract void LoadCargo(int payload);
}