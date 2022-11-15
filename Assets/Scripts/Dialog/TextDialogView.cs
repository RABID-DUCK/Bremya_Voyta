using Ekonomika.Utils;
using TMPro;
using UnityEngine;

namespace Ekonomika.Dialog
{
    public class TextDialogView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI nameText;

        [SerializeField]
        private AnimTextTMPUGUI mainAnimText;

        public void SetRepica(Replica replica)
        {
            nameText.text = replica.person.name;
            mainAnimText.WriteText(replica.text);
        }
    }
}
