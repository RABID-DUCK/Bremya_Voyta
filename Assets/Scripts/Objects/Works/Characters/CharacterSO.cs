using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class CharacterSO : ScriptableObject
{
    [Header("Информация о классе")]
    [Tooltip("Название профессии")] public string nameCharacter;
    [TextArea, Tooltip("Описание")] public string description;

    [Tooltip("Что делает\n\nЛучше располагайте места в той же последовательтности, что и действия!")]
    public List<string> actions;
    [Tooltip("Места работа\n\nЛучше располагайте места в той же последовательтности, что и действия!")]
    public List<string> placesWork;

    [Header("Элементы для создания/использования")]
    [Tooltip("Скины\n\nЛучше располагайте аватары в той же последовательтности, что и префабы!")]
    public List<GameObject> prefabs;
    [Tooltip("Спрайты профессий\n\nЛучше располагайте аватары в той же последовательтности, что и префабы!")]
    public List<Sprite> avatars;

    public string[] full = new string[2] { "", "" };
    [Range(0, 4), Tooltip("Кол-во игроков на класс")] public int countPlayers = 2;
}
