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
        //TODO: ����, + 3��. �� ����. ���-��
    }

    public void EndClearWeatherWithLittleCold() // ���� ����� ����� ��������, ��� ����� �������!!!
    {
        EndClearWeatherWithLittleColdEffect();
    }

    private void EndClearWeatherWithLittleColdEffect() // ����������� ������ ������ �������
    {
        //TODO: ������� � �����
    }
}
