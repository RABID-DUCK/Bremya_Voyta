using UnityEngine;

[RequireComponent(typeof(Character))]
public abstract class Profession : MonoBehaviour
{
    protected Character player;

    private void Awake()
    {
        player = GetComponent<Character>();

        player.OnGetWork += Inject;
    }

    private void OnDestroy()
    {
        player.OnGetWork -= Inject;
    }

    protected abstract void Inject(Work work);
}
