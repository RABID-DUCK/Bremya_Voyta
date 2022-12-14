using System;
using UnityEngine;

namespace Ekonomika.Dialog
{
    public class DialogPresenter : MonoBehaviour
    {
        public event Action OnReplica—hange;
        public static event Action OnDialogEnd;

        public DialogData data { get; private set; }
        
        [SerializeField]
        private TextDialogView textDialogView;

        public int currentReplica
        {
            get
            {
                return _currentReplica;
            }

            set
            {
                _currentReplica = value;
                if (_currentReplica > data.NumOfReplicas - 1)
                {
                    EndDialog();
                }
                else
                {
                    if (data)
                    {
                        OnReplica—hange?.Invoke();
                        textDialogView.SetRepica(data.GetReplica(_currentReplica));
                    }
                }
            }
        }

        private int _currentReplica;

        public void SetDialog(DialogData dialogData)
        {
            data = dialogData;
            currentReplica = 0;
        }

        public void NextReplica()
        {
            currentReplica++;
        }

        public void SetReplica(int replicaId)
        {
            currentReplica = replicaId;
        }

        private void EndDialog()
        {
            //TODO

            OnDialogEnd?.Invoke();
        }
    }
}
