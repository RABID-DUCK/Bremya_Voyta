using Ekonomika.Work;
using UnityEngine;

public class ClearWeatherWithLittleColdEvent : MonoBehaviour
{
    [Tooltip("Scriptable event object \"ClearWeatherWithLittleColdSO\"")]
    public EventSO ClearWeatherWithLittleColdSO;

    public void StartClearWeatherWithLittleCold() // ���� ����� ����� ��������, ��� ������ �������!!!
    {
        StartClearWeatherWithLittleColdEffect();
    }

    private void StartClearWeatherWithLittleColdEffect() // ����������� ������ ������� �� ������
    {
        WorkOverrider.OverrideFishingDropItems(1, 3);

        //TODO: ����, + 3��. �� ����. ���-��
    }

    public void EndClearWeatherWithLittleCold() // ���� ����� ����� ��������, ��� ����� �������!!!
    {
        EndClearWeatherWithLittleColdEffect();
    }

    private void EndClearWeatherWithLittleColdEffect() // ����������� ������ ������ �������
    {
        WorkOverrider.ReturnFishingDropItems();

        //TODO: ������� � �����
    }
}
