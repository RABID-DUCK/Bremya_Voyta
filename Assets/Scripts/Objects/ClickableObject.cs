using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Outline))]
public abstract class ClickableObject : MonoBehaviour, IClickableObject
{
    private Outline outline;

    private void Start()
    {
        outline = GetComponent<Outline>();
    }

    private void OnMouseOver()
    {
        outline.enabled = true;
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }

    public abstract void Exicute(Character player);
}
