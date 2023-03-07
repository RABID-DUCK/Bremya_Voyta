using Ekonomika.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickEventer : MonoBehaviour, IObjectWithCharacter
{
    public System.Action OnClick;

    private List<IClickableObject> clickableObjects = new List<IClickableObject>();
    
    private IObjectClickBehavior _objectClickBehavior;
    private Character _player;
    //public AudioSource clickSound;

    private void OnEnable()
    {
        _objectClickBehavior = GetComponent<IObjectClickBehavior>();

        if (_objectClickBehavior == null)
        {
            Debug.LogError($"Not found click behavior!");
            this.enabled = false;
            
            return;
        }

        if (_player)
        {
            _objectClickBehavior.SetPlayer(_player);
        }
    }

    private void Awake()
    {
        SearchClickableObjectsBySceneObjects(FindObjectsOfType<Object>());
    }

    private void Update()
    {
        bool clickOnUi = EventSystem.current != null && !EventSystem.current.IsPointerOverGameObject();

        if (Input.GetMouseButtonDown(0) && clickOnUi)
        {
            Ray ray = CameraSwitch.currentCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            RaycastHit _hit;

            if (Physics.Raycast(ray, out _hit, Mathf.Infinity))
            {
                GetClickableObject(_hit);
            }
        }
        //if (Input.GetMouseButton(0) && !clickSound.isPlaying) clickSound.Play();
    }

    public void InitializePlayer(Character player)
    {
        _player = player;
        _objectClickBehavior?.SetPlayer(_player);
    }

    public void SetObjectsEnabled(bool value)
    {
        foreach (IClickableObject obj in clickableObjects)
        {
            obj.Enabled = value;
        }
    }

    private void GetClickableObject(RaycastHit _hit)
    {
        IClickableObject click = _hit.collider.GetComponent<IClickableObject>();
        
        if (click != null)
        {
            _objectClickBehavior.OnObjectClick(click);
            OnClick?.Invoke();
        }
    }

    private void SearchClickableObjectsBySceneObjects(Object[] sceneObjects)
    {
        foreach (Object obj in sceneObjects)
        {
            if (obj is IClickableObject)
            {
                clickableObjects.Add((IClickableObject)obj);
            }
        }
    }
}
