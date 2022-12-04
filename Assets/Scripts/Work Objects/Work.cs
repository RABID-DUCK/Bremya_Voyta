using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Work : MonoBehaviour
{
    public abstract void Execute();
}
