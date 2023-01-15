using UnityEngine;

[RequireComponent(typeof(Character))]
public abstract class Profession<T> : MonoBehaviour, IProfession where T : IClickableObject
{
    public bool CheckProfessionObject(IClickableObject clickableObject)
    {
        return Equals(clickableObject.GetType(), typeof(T));
    }
}
