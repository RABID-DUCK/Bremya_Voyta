using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Click();
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }
    private void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(3);
        }
    }
}
