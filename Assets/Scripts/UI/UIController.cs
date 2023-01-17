using System;
using UnityEngine;
using UnityEngine.Video;

public class UIController : MonoBehaviour
{
    private const string dialogWindowPrefabName = "DialogWindow";
    private const string infoWindowPrefabName = "InfoWindow";
    private const string videoWindowPrefabName = "VideoWindow";

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
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public static void ShowDialog(string messageText, string applyButtonText, string cancelButtonText,
        Action OnApplyCallback, Action OnCancelCallback = null)
    {
        DialogWindow createdDialogWindow = WindowManager.InitWindowPrefab<DialogWindow>(dialogWindowPrefabName, instance.canvasTransform);
        createdDialogWindow.ShowDialog(messageText, applyButtonText, cancelButtonText, OnApplyCallback, OnCancelCallback);
    }

    public static void ShowYesNoDialog(string messageText, Action OnApplyCallback, Action OnCancelCallback = null)
    {
        ShowDialog(messageText, "Да", "Нет", OnApplyCallback, OnCancelCallback);
    }

    public static void ShowInfo(string messageText, string applyButtonText, Action OnApplyCallback = null)
    {
        InfoWindow createdinfoWindow = WindowManager.InitWindowPrefab<InfoWindow>(infoWindowPrefabName, instance.canvasTransform);
        createdinfoWindow.ShowInfo(messageText, applyButtonText, OnApplyCallback);
    }

    public static void ShowOkInfo(string messageText, Action OnApplyCallback = null)
    {
        ShowInfo(messageText, "Ок", OnApplyCallback);
    }

    public static void ShowVideo(VideoClip video, Action OnEndVideo)
    {
        WindowManager.InitWindowPrefab<VideoWindow>(videoWindowPrefabName, instance.canvasTransform).ShowVideo(video, OnEndVideo);
    }
}
