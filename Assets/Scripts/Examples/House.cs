using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    Outline outline;

    void Start()
    {
        outline = transform.gameObject.GetComponent<Outline>();
    }

    private void OnMouseOver()
    {
        outline.enabled = true;
        Debug.Log("ZALUPINSK");
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }
}
