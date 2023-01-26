using Ekonomika.Work;
using UnityEngine;

public class ClearWeatherWithLittleColdEvent : MonoBehaviour
{
    [Tooltip("Scriptable event object \"ClearWeatherWithLittleColdSO\"")]
    public EventSO ClearWeatherWithLittleColdSO;

    public void StartClearWeatherWithLittleCold() // Этот метод нужно вызывать, при старте события!!!
    {
        StartClearWeatherWithLittleColdEffect();
    }

    private void StartClearWeatherWithLittleColdEffect() // Реализовать логику эффекта от ивента
    {
        WorkOverrider.OverrideFishingDropItems(1, 3);

        //TODO: Пруд, + 3шт. от макс. кол-ва
    }

    public void EndClearWeatherWithLittleCold() // Этот метод нужно вызывать, при конце события!!!
    {
        EndClearWeatherWithLittleColdEffect();
    }

    private void EndClearWeatherWithLittleColdEffect() // Реализовать логику снятия эффекта
    {
        WorkOverrider.ReturnFishingDropItems();

        //TODO: Вернуть к норме
    }
}
