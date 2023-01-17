using UnityEngine;
using UnityEngine.Video;

public abstract class Work : ClickableObject, IWork
{
    [SerializeField]
    private VideoClip workVideo;

    [SerializeField]
    private int standartMinDropItems = 3;

    [SerializeField]
    private int standartMaxDropItems = 5;

    private bool ovverideDropItems = false;
    private int minDropItems;
    private int maxDropItems;

    public void OvverideStandartDropItems(int min, int max)
    {
        ovverideDropItems = true;
        minDropItems = min;
        maxDropItems = max;
    }

    public void ReturnStandartDropItems()
    {
        ovverideDropItems = false;
    }

    public override void Execute(Character player)
    {
        UIController.ShowVideo(workVideo, () =>
        {
            WriteItemsInPlayerInventory(player, CalculateDropItems());
        });
    }

    public void ShowWork()
    {

    }

    protected abstract void WriteItemsInPlayerInventory(Character player, int itemsCount);

    private int CalculateDropItems()
    {
        if (ovverideDropItems)
        {
            return Random.Range(minDropItems, maxDropItems);
        }
        else
        {
            return Random.Range(standartMinDropItems, standartMaxDropItems);
        }
    }
}
