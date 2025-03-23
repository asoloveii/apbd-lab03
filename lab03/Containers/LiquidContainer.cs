using lab03.Exceptions;

namespace lab03.Containers;

public class LiquidContainer : Container, IHazardNotifier
{
    private bool IsHazardous { get; }

    public LiquidContainer(int maxPayload, bool isHazardous) : base('L', maxPayload)
    {
        IsHazardous = isHazardous;
    }

    public void NotifyHazard()
    {
        Console.WriteLine($"[Hazard Alert] Liquid Container {SerialNumber}");
    }

    public override void EmptyCargo()
    {
        CargoWeight = 0;
    }

    public override void LoadCargo(int payload)
    {
        if (IsHazardous && (payload + CargoWeight > MaxPayload * 0.5))
            throw new OverfillException("[Exception] Cannot load hazardous liquid beyond 50% capacity");

        if (!IsHazardous && (payload + CargoWeight > MaxPayload * 0.9))
            throw new OverfillException("[Exception] Cannot load liquid beyond 90% capacity");

        CargoWeight += payload;

    }
}