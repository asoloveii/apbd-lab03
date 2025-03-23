using lab03.Exceptions;

namespace lab03.Containers;

public class GasContainer : Container, IHazardNotifier
{
    private bool IsHazardous { get; }
    private int Pressure { get; set; } // in atmospheres
    
    public GasContainer(int maxPayload, int pressure, bool isHazardous) : base('G', maxPayload)
    {
        IsHazardous = isHazardous;
        Pressure = pressure;
    }

    public void NotifyHazard()
    {
        Console.WriteLine($"[Hazard Alert] Gas Container {SerialNumber}");
    }

    public override void EmptyCargo()
    {
        CargoWeight = (int)(CargoWeight * 0.05);
    }

    public override void LoadCargo(int payload)
    {
        if (CargoWeight + payload > MaxPayload) throw new OverfillException("[Exception] Gas Container overflow");
        CargoWeight += payload;
    }
}