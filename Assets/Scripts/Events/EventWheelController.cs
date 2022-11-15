using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventWheelController : MonoBehaviour
{
    [SerializeField] private Events events;

    public void SelectRandomEvents()
    {
        int randomNum = Random.Range(1, 6);

        RandomEvent(randomNum);
    }

    public void RandomEvent(int currenEvent)
    {
        switch (currenEvent)
        {
            case 1:

                events.LittleRain();

                break;

            case 2:

                events.ClearWeatherWithLittleCold();

                break;

            case 3:

                events.StandartDay();

                break;

            case 4:

                events.Mine—ollapse();

                break;
            case 5:

                events.ThunderstormWithHeavyRain();

                break;

            case 6:

                events.Shtorm();

                break;
        }
    }
}