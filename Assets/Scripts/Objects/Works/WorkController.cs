using System.Collections.Generic;
using UnityEngine;

public class WorkController : MonoBehaviour
{
    [SerializeField] private List<BerryPickerWork> berryPickerWorks;
    [SerializeField] private List<BreederWork> breederWorks;
    [SerializeField] private List<CoalMiningWork> coalMiningWorks;
    [SerializeField] private List<FarmerWork> farmerWorks;
    [SerializeField] private List<FishingWork> fishingWorks;
    [SerializeField] private List<HuntingWork> huntingWorks;
    [SerializeField] private List<IronMiningWork> ironMiningWorks;
    [SerializeField] private List<LumberjackWork> lumberjackWorks;

    public void OvverideBerryPickerWorksDropItems(int min, int max)
    {
        OvverideDropItems(berryPickerWorks.ToArray(), min, max);
    }

    public void ReturnBerryPickerWorksDropItems()
    {
        ReturnDropItems(berryPickerWorks.ToArray());
    }

    public void OvverideBreederDropItems(int min, int max)
    {
        OvverideDropItems(breederWorks.ToArray(), min, max);
    }

    public void ReturnBreederDropItems()
    {
        ReturnDropItems(breederWorks.ToArray());
    }

    public void OvverideCoalMiningDropItems(int min, int max)
    {
        OvverideDropItems(coalMiningWorks.ToArray(), min, max);
    }

    public void ReturnCoalMiningDropItems()
    {
        ReturnDropItems(coalMiningWorks.ToArray());
    }

    public void OvverideFarmerDropItems(int min, int max)
    {
        OvverideDropItems(farmerWorks.ToArray(), min, max);
    }

    public void ReturnFarmerDropItems()
    {
        ReturnDropItems(farmerWorks.ToArray());
    }

    public void OvverideFishingDropItems(int min, int max)
    {
        OvverideDropItems(fishingWorks.ToArray(), min, max);
    }

    public void ReturnFishingDropItems()
    {
        ReturnDropItems(fishingWorks.ToArray());
    }

    public void OvverideHuntingDropItems(int min, int max)
    {
        OvverideDropItems(huntingWorks.ToArray(), min, max);
    }

    public void ReturnHuntingDropItems()
    {
        ReturnDropItems(huntingWorks.ToArray());
    }

    public void OvverideIronMiningDropItems(int min, int max)
    {
        OvverideDropItems(ironMiningWorks.ToArray(), min, max);
    }

    public void ReturnLumberjackDropItems()
    {
        ReturnDropItems(ironMiningWorks.ToArray());
    }

    public void OvverideLumberjackDropItems(int min, int max)
    {
        OvverideDropItems(lumberjackWorks.ToArray(), min, max);
    }

    public void ReturnIronMiningDropItems()
    {
        ReturnDropItems(lumberjackWorks.ToArray());
    }

    private void OvverideDropItems(Work[] works, int min, int max)
    {
        foreach (Work work in works)
        {
            work.OvverideStandartDropItems(min, max);
        }
    }

    private void ReturnDropItems(Work[] works)
    {
        foreach (Work work in works)
        {
            work.ReturnStandartDropItems();
        }
    }
}
