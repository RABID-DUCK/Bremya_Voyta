using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoWindow : WindowBehaviour
{
    private Action OnVideoEnd;

    [SerializeField]
    private VideoPlayer videoPlayer;

    [SerializeField]
    private RawImage rawImage;

    private RectTransform rectTransform;
    private RenderTexture renderTexture;

    protected override void Start()
    {
        base.Start();

        rectTransform = transform as RectTransform;

        renderTexture = new RenderTexture((int)rectTransform.rect.width, (int)rectTransform.rect.height, 24);
        videoPlayer.targetTexture = renderTexture;

        rawImage.texture = renderTexture;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        StopAllCoroutines();
    }

    public void ShowVideo(VideoClip video, Action OnVideoEnd)
    {
        this.OnVideoEnd = OnVideoEnd;
        StartCoroutine(StartVideo(video));
    }

    private IEnumerator StartVideo(VideoClip videoClip)
    {
        videoPlayer.clip = videoClip;
        yield return new WaitForSeconds((float)videoClip.length);
        OnVideoEnd?.Invoke();
        showCanvasGroup.Hide();
    }
}
