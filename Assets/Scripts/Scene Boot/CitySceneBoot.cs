using Cinemachine;
using Ekonomika.Dialog;
using System.Collections.Generic;
using UnityEngine;

public class CitySceneBoot : MonoBehaviour
{
    public static System.Action OnEndEducation;
    
    [SerializeField] private CitySceneUICoordinator UICoordinator;
    [SerializeField] private ClickEventer clickEventer;
    [SerializeField] private DialogData startDialogData;
    [SerializeField] private DialogPresenter dialogPresenter;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform startCameraTransform;


    private List<IObjectWithCharacter> sceneObjectsWithCharacter = new List<IObjectWithCharacter>();

    private Character _player;

    private void Awake()
    {
        SearchByObjects(FindObjectsOfType<Object>());
    }

    public void BootScene(Character character, bool startDialog = true)
    {
        _player = character;

        foreach (IObjectWithCharacter sceneObj in sceneObjectsWithCharacter)
        {
            sceneObj.InitializePlayer(_player);
        }

        virtualCamera.Follow = startCameraTransform;

        clickEventer.SetObjectsEnabled(false);
        dialogPresenter.OnDialogEnd += EndEducation;

        if (startDialog)
        {
            dialogPresenter.StartDialog(startDialogData);
            _player.SetMovement(false);
        }
        else
        {
            EndEducation();
        }
    }

    private void SearchByObjects(Object[] objects)
    {
        foreach (Object obj in objects)
        {
            switch (obj)
            {
                case IObjectWithCharacter:
                    sceneObjectsWithCharacter.Add((IObjectWithCharacter)obj);
                    break;
            }
        }
    }

    private void EndEducation()
    {
        dialogPresenter.OnDialogEnd -= EndEducation;

        virtualCamera.Follow = _player.transform;
        UICoordinator.Subscribe();
        clickEventer.SetObjectsEnabled(true);

        _player.SetMovement(true);

        OnEndEducation?.Invoke();
    }
}
