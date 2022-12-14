using Ekonomika.Dialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEventWheel : MonoBehaviour
{
    [SerializeField] private RectTransform arrowRectTransform;

    [SerializeField, Range(0, 1800)] private static float arrowStroke;

    private Vector3 arrowRotation;

    private bool IsStartArrowStroke;

    private void Start()
    {
        DialogPresenter.OnDialogEnd += StartArrowStroke;
    }

    public void StartArrowStroke()
    {
        IsStartArrowStroke = true;
    }

    private void Update()
    {
        //if (IsStartArrowStroke)  // Раскомментировать условие, когда подключите диалоговую систему
        //{

        if(arrowStroke == 1800)
        {
            arrowStroke = 0;
        }

        arrowRectTransform.Rotate(0, 0, -arrowStroke - 1 * Time.deltaTime);

        //}
    }
}
