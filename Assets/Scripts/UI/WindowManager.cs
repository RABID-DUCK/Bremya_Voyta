using System.IO;
using UnityEngine;

public class WindowManager
{
    private const string prefabFolderInResources = "Windows";

    public static GameObject InitWindowPrefab(string namePrefab, Transform canvasTransform) 
    {
        Object initPrefab = Resources.Load(Path.Combine(prefabFolderInResources, namePrefab), typeof(GameObject));
        return InitWindow(initPrefab, canvasTransform);
    }

    public static T InitWindowPrefab<T>(string namePrefab, Transform canvasTransform) where T : WindowBehaviour
    {
        return InitWindowPrefab(namePrefab, canvasTransform).GetComponent<T>();
    }

    private static GameObject InitWindow(Object initPrefab, Transform canvasTransform)
    {
        GameObject window = (GameObject)GameObject.Instantiate(initPrefab, canvasTransform);
        return window;
    }
}
