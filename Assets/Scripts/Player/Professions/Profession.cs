using UnityEngine;

[RequireComponent(typeof(Character))]
public abstract class Profession<T> : MonoBehaviour where T : IClickableObject
{
    private Character player;

    private void Awake()
    {
        player = GetComponent<Character>();

        player.OnGetWork += Inject;
    }

    private void OnDestroy()
    {
        player.OnGetWork -= Inject;
    }

    public void Inject(IClickableObject work)
    {
        if (Equals(work.GetType(), typeof(T)))
        {
            work.Exicute(player);
        }
    }
}
