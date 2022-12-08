using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterItem : MonoBehaviour
{
    // |0   ��������   |1   ��������   |2   �������� 1   |3   �������� 2   |4   ������� 1   |5   ������� 2   | 
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
