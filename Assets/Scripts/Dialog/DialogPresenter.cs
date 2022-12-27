using System;
using UnityEngine;

namespace Ekonomika.Dialog
{
    [RequireComponent(typeof(CanvasGroup), typeof(ShowCanvasGroup))]
    public class DialogPresenter : MonoBehaviour
    {
        public event Action OnDialogEnd;
        public event Action OnReplicaChange;

        public DialogData data { get; private set; }

        [SerializeField]
        private TextDialogView textDialogView;

        private ShowCanvasGroup showCanvasGroup;

        private void Start()
        {
            showCanvasGroup = GetComponent<ShowCanvasGroup>();
            showCanvasGroup.FastHide();
        }

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
                        OnReplicaChange?.Invoke();
                        textDialogView.SetRepica(data.GetReplica(_currentReplica));
                    }
                }
            }
        }

        private int _currentReplica;

        public void StartDialog(DialogData dialogData)
        {
            data = dialogData;
            currentReplica = 0;
            showCanvasGroup.Show();
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
            showCanvasGroup.Hide();
            OnDialogEnd?.Invoke();
        }
    }
}
