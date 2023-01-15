using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider), typeof(Outline))]
public abstract class ClickableObject : MonoBehaviour, IClickableObject
{
    private Outline outline;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    private void OnMouseOver()
    {
        bool clickOnUi = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        
        if (!clickOnUi)
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }

    public abstract void Execute(Character player);
}
