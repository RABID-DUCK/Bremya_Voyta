using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [Range(0, 6), Tooltip("Player's count on room")] public int countPlayersRoom = 6;
    public List<string> nick = new List<string> { "Jazz", "Alex", "Choon", "Jenorer", "Frin", "Qwano" };
    public List<CharacterSO> listCharacters;
}
