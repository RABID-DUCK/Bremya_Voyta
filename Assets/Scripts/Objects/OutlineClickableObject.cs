using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider), typeof(Outline))]
public abstract class OutlineClickableObject : MonoBehaviour, IObjectWithCharacter, IClickableObject
{
    public bool Enabled { get; set; } = true;
    public Character Player { get => _player; }

    private Outline outline;
    private Character _player;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    private void OnMouseOver()
    {
        if (Enabled)
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
    }

    private void OnMouseExit()
    {
        if (Enabled)
        {
            outline.enabled = false;
        }
    }

    public void InitializePlayer(Character player)
    {
        _player = player;
    }

    public abstract void Execute();
}
