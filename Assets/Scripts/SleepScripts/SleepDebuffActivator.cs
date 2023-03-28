using Ekonomika.Work;
using UnityEngine;

public class SleepDebuffActivator : MonoBehaviour, IObjectWithCharacter
{
    [SerializeField] private SleepPresenter sleepPresenter;

    private Character character;

    public void InitializePlayer(Character player)
    {
        character = player;
    }

    private void Start()
    {
        sleepPresenter.OnAddDebuffToPlayer += AddDebuff;

        sleepPresenter.OnRemoveDebuffFromPlayer += ResetDebuff;
    }

    private void AddDebuff() //Говнокод 1
    {
        switch (character.Type)
        {
            case CharacterType.Farmer:
                {
                    WorkOverrider.OverrideFarmerDropItems(1, 3);
                    WorkOverrider.OverrideBreederDropItems(1, 3);
                }
                break;

            case CharacterType.Hunter:
                {
                    WorkOverrider.OverrideHuntingDropItems(1, 3);
                    WorkOverrider.OverrideFishingDropItems(1, 3);
                }
                break;

            case CharacterType.Lumberjack:
                {
                    WorkOverrider.OverrideLumberjackDropItems(1, 3);
                    WorkOverrider.OverrideBerryPickerWorksDropItems(1, 3);
                }
                break;

            case CharacterType.Miner:
                {
                    WorkOverrider.OverrideIronMiningDropItems(1, 3);
                    WorkOverrider.OverrideCoalMiningDropItems(1, 3);
                }
                break;
        }
    }

    private void ResetDebuff() // Говнокод 2
    {
        switch(character.Type)
        {
            case CharacterType.Farmer:
                {
                    WorkOverrider.ReturnFarmerDropItems();
                    WorkOverrider.ReturnBreederDropItems();
                }
                break;

            case CharacterType.Hunter:
                {
                    WorkOverrider.ReturnHuntingDropItems();
                    WorkOverrider.ReturnFishingDropItems();
                }
                break;

            case CharacterType.Lumberjack:
                {
                    //WorkOverrider.ReturnLumberjackDropItems();
                    WorkOverrider.OverrideLumberjackDropItems(3, 5);
                    WorkOverrider.ReturnBerryPickerWorksDropItems();
                }
                break;

            case CharacterType.Miner:
                {
                    WorkOverrider.ReturnIronMiningDropItems();
                    WorkOverrider.ReturnCoalMiningDropItems();
                }
                break;
        }
    }

    private void OnDestroy()
    {
        sleepPresenter.OnAddDebuffToPlayer -= AddDebuff;

        sleepPresenter.OnRemoveDebuffFromPlayer -= ResetDebuff;
    }
}
