using System;
using UnityEditor;
using UnityEngine;

public class WorldTime : MonoBehaviour
{
    [Header("Time of day settings")]
    [Tooltip("Number of seconds in one day")]
    [Range(0, 260)] public float timeDayInSeconds;
    [Tooltip("Time of day range")]
    [Range(0f, 1f)] public float timeProgress;

    [Space, Tooltip("Count of days elapsed")]
    public int countOfDaysElapsed;

    public static event Action isDay;

    public static event Action isNight;

    private void Update()
    {
        ChengeOfTime();

        if (timeDayInSeconds == 40)
        {
            isDay?.Invoke();
        }

        if (timeDayInSeconds == 220)
        {
            isNight?.Invoke();
        }
    }

    private void ChengeOfTime()
    {
        if (timeDayInSeconds == 0)
        {
            print("There can't be 0 seconds in one day!!!");
        }

        if (Application.isPlaying)
        {
            timeProgress += Time.deltaTime / timeDayInSeconds;
        }

        if (timeProgress > 1f)
        {
            timeProgress = 0f;

            countOfDaysElapsed++;
        }
    }
}
