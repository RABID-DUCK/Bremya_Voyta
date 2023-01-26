using UnityEngine;
using UnityEngine.Video;

namespace Ekonomika.Work
{
    public class WorkBehaviour : ClickableObject, IWork
    {
        public string WorkName => _workName;
        public Item ReceivedItem => _receivedItem;

        [Header("Work settings")]
        [SerializeField] private string _workName;
        [SerializeField] private Item _receivedItem;

        [Space]

        [SerializeField] private VideoClip workVideo;

        [Space]

        [SerializeField] private int standartMinDropItems = 3;
        [SerializeField] private int standartMaxDropItems = 5;

        private bool overrideDropItems = false;
        private int minDropItems = 3;
        private int maxDropItems = 5;

        public override void Execute(Character player)
        {
            UIController.ShowYesNoDialog($"Вы хотите начать работу ({WorkName})?", () =>
            {
                UIController.ShowVideo(workVideo, () =>
                {
                    player.Inventory.PutItem(ReceivedItem, CalculateDropItems());
                });
            });
        }

        public void OverrideStandartDropItems(int min, int max)
        {
            overrideDropItems = true;
            minDropItems = min;
            maxDropItems = max;
        }

        public void ReturnStandartDropItems() =>
            overrideDropItems = false;

        private int CalculateDropItems()
        {
            if (overrideDropItems)
            {
                return Random.Range(minDropItems, maxDropItems);
            }
            else
            {
                return Random.Range(standartMinDropItems, standartMaxDropItems);
            }
        }
    }
}
