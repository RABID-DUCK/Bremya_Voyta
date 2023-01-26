using UnityEngine;

namespace Ekonomika.Work
{
    public class WorkOverrider : MonoBehaviour
    {
        private static WorkOverrider instance = null;

        private const string berryItemObjectName = "Berry";
        private const string carrotItemObjectName = "Carrot";
        private const string coalItemObjectName = "Coal";
        private const string fishItemObjectName = "Fish";
        private const string ironItemObjectName = "Iron";
        private const string meatItemObjectName = "Meat";
        private const string milkItemObjectName = "Milk";
        private const string woodItemObjectName = "Wood";

        [SerializeField]
        private WorkController workController;

        private void Awake()
        {
            if (instance && instance != this)
            {
                Destroy(this);
                return;
            }

            instance = this;
        }

        private void OnDestroy()
        {
            instance = null;
        }

        public static void OverrideBerryPickerWorksDropItems(int min, int max)
        {
            instance.OverrideDropItems(berryItemObjectName, min, max);
        }

        public static void ReturnBerryPickerWorksDropItems()
        {
            instance.ReturnDropItems(berryItemObjectName);
        }


        public static void OverrideFarmerDropItems(int min, int max)
        {
            instance.OverrideDropItems(carrotItemObjectName, min, max);
        }

        public static void ReturnFarmerDropItems()
        {
            instance.ReturnDropItems(carrotItemObjectName);
        }


        public static void OverrideCoalMiningDropItems(int min, int max)
        {
            instance.OverrideDropItems(coalItemObjectName, min, max);
        }

        public static void ReturnCoalMiningDropItems()
        {
            instance.ReturnDropItems(coalItemObjectName);
        }


        public static void OverrideFishingDropItems(int min, int max)
        {
            instance.OverrideDropItems(fishItemObjectName, min, max);
        }

        public static void ReturnFishingDropItems()
        {
            instance.ReturnDropItems(fishItemObjectName);
        }

        
        public static void OverrideIronMiningDropItems(int min, int max)
        {
            instance.OverrideDropItems(ironItemObjectName, min, max);
        }

        public static void ReturnIronMiningDropItems(int min, int max)
        {
            instance.ReturnDropItems(ironItemObjectName);
        }

        
        public static void OverrideHuntingDropItems(int min, int max)
        {
            instance.OverrideDropItems(meatItemObjectName, min, max);
        }

        public static void ReturnHuntingDropItems()
        {
            instance.ReturnDropItems(meatItemObjectName);
        }


        public static void OverrideBreederDropItems(int min, int max)
        {
            instance.OverrideDropItems(milkItemObjectName, min, max);
        }

        public static void ReturnBreederDropItems()
        {
            instance.ReturnDropItems(milkItemObjectName);
        }


        public static void OverrideLumberjackDropItems(int min, int max)
        {
            instance.OverrideDropItems(woodItemObjectName, min, max);
        }

        public static void ReturnIronMiningDropItems()
        {
            instance.ReturnDropItems(woodItemObjectName);
        }


        private void OverrideDropItems(string ItemObjectName, int min, int max)
        {
            foreach (WorkBehaviour work in GetWorkByItemObjectName(ItemObjectName))
            {
                work.OverrideStandartDropItems(min, max);
            }
        }

        private void ReturnDropItems(string ItemObjectName)
        {
            foreach (WorkBehaviour work in GetWorkByItemObjectName(ItemObjectName))
            {
                work.ReturnStandartDropItems();
            }
        }

        private WorkBehaviour[] GetWorkByItemObjectName(string name)
        {
            return workController.GetWorkByItemMatch(x =>
            {
                return x.name == name;
            });
        }
    }
}
