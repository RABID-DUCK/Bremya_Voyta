namespace Ekonomika.Utils
{
    using UnityEngine;

    public class CameraSwitch : MonoBehaviour
    {
        public static Camera currentCamera { get; private set; }

        private static CameraSwitch instance = null;

        [SerializeField]
        private Camera _mainCamera;

        [SerializeField]
        private Camera _houseCamera;

        private void Awake()
        {
            if (instance && instance != this)
            {
                Destroy(this);
                return;
            }

            instance = this;
            SwichToMainCamera();
        }

        private void OnDestroy()
        {
            instance = null;
        }

        public static void SwichToMainCamera()
        {
            if (instance)
            {
                instance._houseCamera.gameObject.SetActive(false);
                instance._mainCamera.gameObject.SetActive(true);

                currentCamera = instance._mainCamera;
            }
        }

        public static void SwichHouseCamera()
        {
            if (instance)
            {
                instance._mainCamera.gameObject.SetActive(false);
                instance._houseCamera.gameObject.SetActive(true);
                
                currentCamera = instance._houseCamera;
            }
        }
    }
}
