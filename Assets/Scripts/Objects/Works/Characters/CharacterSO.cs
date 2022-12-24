using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class CharacterSO : ScriptableObject
{
    [Header("���������� � ������")]
    [Tooltip("�������� ���������")] public string nameCharacter;
    [TextArea, Tooltip("��������")] public string description;

    [Tooltip("��� ������\n\n����� ������������ ����� � ��� �� �������������������, ��� � ��������!")]
    public List<string> actions;
    [Tooltip("����� ������\n\n����� ������������ ����� � ��� �� �������������������, ��� � ��������!")]
    public List<string> placesWork;

    [Header("�������� ��� ��������/�������������")]
    [Tooltip("�����\n\n����� ������������ ������� � ��� �� �������������������, ��� � �������!")]
    public List<GameObject> prefabs;
    [Tooltip("������� ���������\n\n����� ������������ ������� � ��� �� �������������������, ��� � �������!")]
    public List<Sprite> avatars;

    [HideInInspector] public List<bool> full;
    [Range(0, 4), Tooltip("���-�� ������� �� �����")] public int countPlayers = 2;

    private ExitGames.Client.Photon.Hashtable _CP = new ExitGames.Client.Photon.Hashtable();
}
