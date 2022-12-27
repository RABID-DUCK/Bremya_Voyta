using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour
{
    public event Action<IClickableObject> OnGetWork;

    [Serializable]
    public struct CharacterInventory
    {
        public int coins;

        public int woodCount;
        public int berriesCount;
        public int carrotCount;
        public int milkCount;
        public int coalCount;
        public int ironCount;
        public int meatCount;
        public int fishCount;
    }

    public CharacterInventory inventory;

    private void Update()
    {
        bool clickOnUi = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        
        if (Input.GetMouseButtonDown(0) && clickOnUi)
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
