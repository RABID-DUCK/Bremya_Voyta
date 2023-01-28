using System;
using UnityEngine;

public class TaxBox : MonoBehaviour
{
    [SerializeField] private GameObject taxBox;

    private Outline outline;

    public event Action OnClickTaxBox;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void OnMouseOver()
    {
        outline.enabled = true;

        if (Input.GetMouseButtonDown(0))
        {
            OnClickTaxBox?.Invoke();
        }
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }
}