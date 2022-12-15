using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public event Action<IClickableObject> OnGetWork;

    [Serializable]
    public struct CharacterInventory
    {
        public int coins;

        public int woodCount;
        public int berriesCount;
        public int milkCount;
        public int metallCount;
        public int meatCount;
        public int fishCount;
        public int carrotCount;
    }

    public CharacterInventory inventory;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            RaycastHit _hit;

            if (Physics.Raycast(ray, out _hit, Mathf.Infinity))
            {
                GetObject(_hit);
            }
        }
    }

    private void GetObject(RaycastHit _hit)
    {
        IClickableObject click = _hit.collider.GetComponent<IClickableObject>();

        if (click != null)
        {
            OnGetWork?.Invoke(click);
        }
    }
}
