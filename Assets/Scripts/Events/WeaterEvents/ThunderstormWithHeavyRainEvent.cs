using Ekonomika.Work;
using System.Collections;
using UnityEngine;

public class ThunderstormWithHeavyRainEvent : MonoBehaviour
{
    [Tooltip("Scriptable event object \"ThunderstormSO\"")]
    public EventSO ThunderSO;

    [Space, Tooltip("The object of the particle system")]
    [SerializeField] private GameObject ThunderstormPS;

    [Tooltip("General Directional light")]
    [SerializeField] private Light directionalLight;

    [Tooltip("Light from Lightning")]
    [SerializeField] private Light spotLight;

    private bool IsStarted { get; set; }
    private float randomTimeStartLighting { get; set; }
    private float timer { get; set; }

    public void StartThunderEvent() // Метод отвечающий за появление молнии.
    {
        StartCoroutine(LightDarkens());

        ThunderstormPS.SetActive(true);

        RandomizeTimeStartLighting();

        IsStarted = true;

        StartThindershtormEffect();
    }

    private void StartThindershtormEffect()
    {
        WorkOverrider.OverrideFishingDropItems(1, 3);
        WorkOverrider.OverrideFarmerDropItems(1, 3);
        WorkOverrider.OverrideHuntingDropItems(1, 3);

        //TODO: Пруд, огород, Степь -2шт. от макс. кол-ва 
    }

    public void EndThunderEvent() // Этот метод нужно вызывать, при конце события!!!
    {
        //StartCoroutine(LightIsBrighter());

        ThunderstormPS.SetActive(false);

        IsStarted = false;

        EndThindershtormEffect();
    }

    private void EndThindershtormEffect()
    {
        WorkOverrider.ReturnFishingDropItems();
        WorkOverrider.ReturnFarmerDropItems();
        WorkOverrider.ReturnHuntingDropItems();

        //TODO: Вернуть к норме
    }

    private IEnumerator LightDarkens()
    {
        for (float i = 1f; i == 0.2f; i -= 0.05f)
        {
            yield return new WaitForSeconds(0.05f);

            directionalLight.intensity = i;
        }
    }

    private IEnumerator LightIsBrighter()
    {
        for (float i = 0.2f; i <= 1; i += 0.05f)
        {
            yield return new WaitForSeconds(0.05f);

            directionalLight.intensity = i;
        }
    }

    private void Update()
    {
        if (IsStarted)
        {
            timer += Time.deltaTime;

            if (randomTimeStartLighting == timer)
            {
                RandomizeTimeStartLighting();

                StartCoroutine(Lightning());
            }
        }
    }

    private void RandomizeTimeStartLighting()
    {
        randomTimeStartLighting = Random.Range(5, 30);
    }

    private IEnumerator Lightning()
    {
        for (int i = 0; i < 2; i++)
        {
            spotLight.intensity = 2;

            yield return new WaitForSeconds(0.02f);

            spotLight.intensity = 1;
        }
    }
}
