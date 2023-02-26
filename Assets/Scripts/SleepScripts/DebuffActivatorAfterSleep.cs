using UnityEngine;

public class DebuffActivatorAfterSleep : MonoBehaviour
{
    [SerializeField] private SleepPresenter sleepPresenter;

    private void Start()
    {
        sleepPresenter.OnAddDebuffToPlayer += AddDebuff;
    }

    private void AddDebuff()
    {
        //TODO: Добавить дебафф
    }
}
