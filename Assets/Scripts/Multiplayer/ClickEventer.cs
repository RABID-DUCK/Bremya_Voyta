using Ekonomika.Work;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickEventer : MonoBehaviour
{
    public event Action<IClickableObject> OnClickObject;
    public event Action<IWork> OnClickWork;

    private void Update()
    {
        bool clickOnUi = EventSystem.current != null && !EventSystem.current.IsPointerOverGameObject();

        if (Input.GetMouseButtonDown(0) && clickOnUi)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            RaycastHit _hit;

            if (Physics.Raycast(ray, out _hit, Mathf.Infinity))
            {
                GetClickableObject(_hit);
            }
        }
    }

    private void GetClickableObject(RaycastHit _hit)
    {
        IClickableObject click = _hit.collider.GetComponent<IClickableObject>();

        if (click != null)
        {
            switch (click)
            {
                default:
                    OnClickObject?.Invoke(click);
                    break;
                
                case IWork:
                    OnClickWork?.Invoke((IWork)click);
                    break;
            }
        }
    }
}
