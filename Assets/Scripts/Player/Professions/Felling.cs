using UnityEngine;

public class Felling : Profession
{
    protected override void Inject(Work work)
    {
        if (work.GetComponent<Forest>())
        {
            work.Execute();

            player.inventory.woodCount += Random.Range(3, 5);
        }
    }
}
