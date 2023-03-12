using System;

public class Bed : OutlineClickableObject
{
    public event Action OnClickOnBed;

    private void SendClickOnBed()
    {
        OnClickOnBed?.Invoke();
    }

    public override void OnExecute()
    {
        SendClickOnBed();
    }
}