using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UITutorialObject : BaseTutorialObject
{
    [SerializeField] private Image highligtedImage;
    [SerializeField] private float highligtSpeed = 0.6f;

    [Range(0, 1)]
    [SerializeField] private float brightnessMax = 1;
    
    [Range(0, 1)]
    [SerializeField] private float brightnessMin = 0.85f;

    private Color defaultColor;

    private void Awake()
    {
        defaultColor = highligtedImage.color;
    }

    protected override IEnumerator HighlightObject()
    {
        float[] hsv = new float[3];
        Color.RGBToHSV(highligtedImage.color, out hsv[0], out hsv[1], out hsv[2]);

        float brightnessValue = hsv[2];
        bool isShowed = !(brightnessValue == brightnessMax);
        while (true)
        {
            float speedToDeltatime = Time.deltaTime * highligtSpeed;

            if (isShowed)
            {
                brightnessValue -= speedToDeltatime;

                isShowed = brightnessValue > brightnessMin;
            }
            else
            {
                brightnessValue += speedToDeltatime;

                isShowed = !(brightnessValue < brightnessMax);
            }

            highligtedImage.color = Color.HSVToRGB(0f, 0f, brightnessValue);

            yield return new WaitForEndOfFrame();
        }
    }

    protected override void OnStopHighlightObject()
    {
        highligtedImage.color = defaultColor;
    }
}
