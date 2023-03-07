using Ekonomika.Dialog;
using System.Collections.Generic;
using UnityEngine;

public class CitySceneBoot : MonoBehaviour
{
    public static System.Action OnEndEducation;
    
    [SerializeField] private CitySceneUICoordinator uiCoordinator;
    [SerializeField] private ClickEventer clickEventer;
    [SerializeField] private DialogData startDialogData;
    [SerializeField] private DialogPresenter dialogPresenter;

    private List<IObjectWithCharacter> sceneObjectsWithCharacter = new List<IObjectWithCharacter>();

    private void Awake()
    {
        SearchByObjects(FindObjectsOfType<Object>());
    }

    private void Start()
    {
        clickEventer.SetObjectsEnabled(false);
        dialogPresenter.OnDialogEnd += EndEducation;
        dialogPresenter.StartDialog(startDialogData);
    }

    public void SetPlayer(Character character)
    {
        foreach (IObjectWithCharacter sceneObj in sceneObjectsWithCharacter)
        {
            sceneObj.InitializePlayer(character);
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

        uiCoordinator.Subscribe();
        clickEventer.SetObjectsEnabled(true);

        OnEndEducation?.Invoke();
    }
}
