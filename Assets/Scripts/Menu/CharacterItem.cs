using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterItem : MonoBehaviour
{
    // |0   Название   |1   Описание   |2   Действия 1   |3   Действия 2   |4   Локация 1   |5   Локация 2   | 
    [SerializeField] private Button btnChange;
    public string Name { get; set; }
    public string Desc { get; set; }
    public string Move1 { get; set; }
    public string Move2 { get; set; }
    public string Locate1 { get; set; }
    public string Locate2 { get; set; }
    public int countPlayers { get; set; }

    public void SetInfo()
    {
        
    }
}
