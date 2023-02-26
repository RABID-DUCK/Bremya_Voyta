using System;

public class Bed : ClickableObject
{
    public event Action OnClickOnBed;

    private void SendClickOnBed()
    {
        OnClickOnBed?.Invoke();
    }

    public override void Execute(Character player)
    {
        SendClickOnBed();
    }
}