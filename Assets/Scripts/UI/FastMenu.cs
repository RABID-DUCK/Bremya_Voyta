using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FastMenu : MonoBehaviour
{
    [Header("Panel settings")]
    [SerializeField] private GameObject menu;

    [SerializeField] private GameObject settingsMenu;

    [Header("Button of menu panel")]
    [SerializeField] private Button ContinueButton;

    [SerializeField] private Button settingsButton;

    [SerializeField] private Button exitInMenuButton;

    [Header("Button of settings menu")]
    [SerializeField] Button settingsMenuBackButton;

    //[SerializeField] private ShowCanvasGroup showCanvasGroup;

    //private void Awake()
    //{
    //    if (showCanvasGroup == null)
    //    {
    //        showCanvasGroup = menu.GetComponent<ShowCanvasGroup>();
    //    }
    //}

    private void Start()
    {
        //BlockRaycastTrue();

        ContinueButton.onClick.AddListener(HideMenu);
        settingsButton.onClick.AddListener(ShowSettingsMenu);
        exitInMenuButton.onClick.AddListener(ExitInMenu);

        settingsMenuBackButton.onClick.AddListener(HideSettingsMenu);
    }

    private void Update()
    {
        ShowMenu();
    }

    private void ShowSettingsMenu()
    {
        settingsMenu.SetActive(true);
    }

    private void HideSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }

    private void ExitInMenu()
    {
        SceneManager.LoadScene("Menu"); // Если возникла ошибка, то я ебал рот того, кто переименовал сцену главного меню!
    }

    private void ShowMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(true);
        }
    }

    private void HideMenu()
    {
        menu.SetActive(false);
    }

    //private void BlockRaycastTrue()
    //{
    //    showCanvasGroup.canvasGroup.blocksRaycasts = true;
    //}

    //private void BlockRaycastFalse()
    //{
    //    showCanvasGroup.canvasGroup.blocksRaycasts = false;
    //}

    //public void OnDestroy()
    //{
    //    showCanvasGroup.OnShowed -= BlockRaycastFalse;
    //    showCanvasGroup.OnHided -= BlockRaycastTrue;
    //}
}