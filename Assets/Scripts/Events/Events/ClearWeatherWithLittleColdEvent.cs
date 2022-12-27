using UnityEngine;

public class ClearWeatherWithLittleColdEvent : MonoBehaviour
{
    [Tooltip("Scriptable event object \"ClearWeatherWithLittleColdSO\"")]
    public EventSO ClearWeatherWithLittleColdSO;

    [Header("Event Property Controller")]
    [SerializeField] private WorkController workController;

    public void StartClearWeatherWithLittleCold() // Этот метод нужно вызывать, при старте события!!!
    {
        StartClearWeatherWithLittleColdEffect();
    }

    private void StartClearWeatherWithLittleColdEffect() // Реализовать логику эффекта от ивента
    {
        workController.OvverideFishingDropItems(1, 3);

        //TODO: Пруд, + 3шт. от макс. кол-ва
    }

    public void EndClearWeatherWithLittleCold() // Этот метод нужно вызывать, при конце события!!!
    {
        EndClearWeatherWithLittleColdEffect();
    }

    private void EndClearWeatherWithLittleColdEffect() // Реализовать логику снятия эффекта
    {
        workController.ReturnFishingDropItems();

        //TODO: Вернуть к норме
    }
}
