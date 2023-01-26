using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ekonomika.Work
{
    public class WorkController : MonoBehaviour
    {
        private Dictionary<Item, List<WorkBehaviour>> works = new Dictionary<Item, List<WorkBehaviour>>();

        private void Awake()
        {
            foreach (WorkBehaviour item in FindObjectsOfType<WorkBehaviour>())
            {
                bool check = false;
                foreach (var workType in works)
                {
                    if (check = item == workType.Key)
                    {
                        workType.Value.Add(item);
                        break;
                    }
                }

                if (!check)
                {
                    List<WorkBehaviour> w = new List<WorkBehaviour>();
                    w.Add(item);
                    works.Add(item.ReceivedItem, w);
                }
            }
        }

        public WorkBehaviour[] GetWorkByItemMatch(Predicate<Item> match)
        {
            foreach (var element in works)
            {
                if (match(element.Key))
                {
                    return element.Value.ToArray();
                }
            }

            return null;
        }
    }
}
