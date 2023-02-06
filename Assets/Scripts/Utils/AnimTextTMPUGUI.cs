using System.Collections;
using TMPro;
using UnityEngine;

namespace Ekonomika.Utils
{
    public class AnimTextTMPUGUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI textObject;

        [SerializeField]
        private float letterPause = 0.01f;

        public void WriteText(string text)
        {
            StopAllCoroutines();
            StartCoroutine(TextAnim(text));
        }

        private IEnumerator TextAnim(string text)
        {
            textObject.text = "";

            foreach (string letter in text.Split(" "))
            {
                textObject.text += letter + " ";
                yield return new WaitForSeconds(letterPause);
            }
        }
    }
}
