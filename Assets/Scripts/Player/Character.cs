using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public event Action<Work> OnGetWork;

    [Serializable]
    public struct CharacterInventory
    {
        public int coins;

        public int woodCount;
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
                GetWork(_hit);
            }
        }
    }

    private void GetWork(RaycastHit _hit)
    {
        Work tempWork = _hit.collider.GetComponent<Work>();

        if (tempWork)
        {
            OnGetWork?.Invoke(tempWork);
        }
    }
}
