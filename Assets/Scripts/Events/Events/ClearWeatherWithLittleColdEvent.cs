using UnityEngine;

public class ClearWeatherWithLittleColdEvent : MonoBehaviour
{
    [Tooltip("Scriptable event object \"ClearWeatherWithLittleColdSO\"")]
    public EventSO ClearWeatherWithLittleColdSO;

    [Header("Event Property Controller")]
    [SerializeField] private WorkController workController;

    public void StartClearWeatherWithLittleCold() // ���� ����� ����� ��������, ��� ������ �������!!!
    {
        StartClearWeatherWithLittleColdEffect();
    }

    private void StartClearWeatherWithLittleColdEffect() // ����������� ������ ������� �� ������
    {
        workController.OvverideFishingDropItems(1, 3);

        //TODO: ����, + 3��. �� ����. ���-��
    }

    public void EndClearWeatherWithLittleCold() // ���� ����� ����� ��������, ��� ����� �������!!!
    {
        EndClearWeatherWithLittleColdEffect();
    }

    private void EndClearWeatherWithLittleColdEffect() // ����������� ������ ������ �������
    {
        workController.ReturnFishingDropItems();

        //TODO: ������� � �����
    }
}
