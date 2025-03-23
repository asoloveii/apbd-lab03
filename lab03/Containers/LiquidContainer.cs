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
        if (IsHazardous)
        {
            if (payload + CargoWeight > MaxPayload * 0.5)
                Console.WriteLine("[Alert] Attempting to load dangerous amount");
        } else if (payload + CargoWeight > MaxPayload * 0.9) 
            Console.WriteLine("[Alert] Attempting to load dangerous amount");
        else CargoWeight += payload;
    }
}