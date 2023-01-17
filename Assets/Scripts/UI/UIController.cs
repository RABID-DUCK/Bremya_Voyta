using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private DialogWindow dialogWindowPrefab;
    private Transform canvasTransform;


    private static UIController instance = null;

    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(this);
            return;
        }

        canvasTransform = FindObjectOfType<Canvas>().transform;

        //if (!canvasTransform)
        //{

        //}
        
        instance = this;
        print(instance);
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public static void ShowDialog(string titleText, string messageText,
        string applyButtonText, string cancelButtonText,
        Action OnApplyCallback, Action OnCancelCallback = null)
    {
        DialogWindow tempDialogWindow = Instantiate(instance.dialogWindowPrefab, instance.canvasTransform);
        tempDialogWindow.ShowDialog(titleText, messageText, applyButtonText, cancelButtonText, OnApplyCallback, OnCancelCallback);

    }

    public static void ShowYesNoDialog(string titleText, string messageText, Action OnApplyCallback, Action OnCancelCallback = null)
    {
        ShowDialog(titleText, messageText, "Да", "Нет", OnApplyCallback, OnCancelCallback);
    }
}
