using Cinemachine;
using Ekonomika.Dialog;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScenario : MonoBehaviour
{
    public Action OnEndScenario;

    [Serializable]
    private struct Timing
    {
        public int replicaId;

        [Header("Zoom Object")]
        [SerializeField] private bool zoomObject;
        [SerializeField] private Transform zoomedObject;
        [SerializeField] private float cameraFieldOfView;

        [Header("Highlight Object")]
        [SerializeField] private bool highlightObject;
        [SerializeField] private BaseTutorialObject learningObject;

        public void ZoomObject(CinemachineVirtualCamera camera)
        {
            if (zoomObject)
            {
                camera.Follow = zoomedObject;
                camera.m_Lens.FieldOfView = cameraFieldOfView;
            }
        }

        public void HighlightObject()
        {
            if (highlightObject)
            {
                learningObject.StartHighlightObject();
            }
        }

        public void StopHighlightObject()
        {
            if (highlightObject)
            {
                learningObject.StopHighlightObject();
            }
        }

    }

    [Header("Dialog")]
    [SerializeField] private DialogPresenter dialogWindow;
    [SerializeField] private DialogData dialogData;

    [Space]

    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [Space]

    [SerializeField] private List<Timing> timings;
    private int currentTimingId;

    private void Start()
    {
        currentTimingId = -1;

        dialogWindow.OnReplicaChange += OnReplicaChange;
        dialogWindow.OnDialogEnd += OnEndDialog;

        dialogWindow.StartDialog(dialogData);
    }

    private void OnReplicaChange()
    {
        StopHighlightCurrentObject();

        for (int i = 0; i < timings.Count; i++)
        {
            if (dialogWindow.currentReplica == timings[i].replicaId)
            {
                timings[i].ZoomObject(virtualCamera);
                timings[i].HighlightObject();

                currentTimingId = i;
            }
        }
    }

    private void StopHighlightCurrentObject()
    {
        if (currentTimingId != -1)
        {
            timings[currentTimingId].StopHighlightObject();
            currentTimingId = -1;
        }
    }

    private void OnEndDialog()
    {
        StopHighlightCurrentObject();
        OnEndScenario?.Invoke();
    }
}
