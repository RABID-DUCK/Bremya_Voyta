using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Message : MonoBehaviour
{
    [SerializeField] public TMP_Text MyMessage;
    void Start()
    {
        GetComponent<RectTransform>().SetAsFirstSibling();        
    }
}
