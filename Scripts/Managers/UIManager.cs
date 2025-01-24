using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public static int ScreenWidth = 1920;
    public static int ScreenHeight = 1080;

    private static Transform uiTransform;
    public static Transform UITransform
    {
        get
        {
            if (uiTransform == null)
            {
                var scenebase = FindFirstObjectByType<SceneBase>();

                if (scenebase == null) 
                {
                    return null;
                }
                uiTransform = scenebase.uiTransform;

            }
            return uiTransform;
        }
        set { uiTransform = value; }
    } 

    private Dictionary<string, UIBase> uiList = new Dictionary<string, UIBase>();

    public T Show<T>(params object[] param) where T : UIBase
    {
        RemoveNull();

        string uiName = typeof(T).ToString();
        uiList.TryGetValue(uiName, out UIBase ui); 
        if (ui == null)
        {
            uiList.Remove(uiName);
            var obj = ResourceManager.Instance.LoadAsset<GameObject>(uiName, eAssetType.UI);
            ui = Load<T>(obj, uiName);
            uiList.Add(uiName, ui);
            ui.Opened(param);
        }

        ui.gameObject.SetActive(true);

        return (T)ui;
    }

    public T Load<T>(GameObject prefab, string uiName) where T : UIBase
    {
        var newCanvasObject = new GameObject(uiName + " Canvas");

        newCanvasObject.transform.SetParent(UIManager.UITransform);
        var canvas = newCanvasObject.gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        var canvasScaler = newCanvasObject.gameObject.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(UIManager.ScreenWidth, UIManager.ScreenHeight);
        newCanvasObject.gameObject.AddComponent<GraphicRaycaster>();

        var obj = Instantiate(prefab, newCanvasObject.transform);
        obj.name = obj.name.Replace("(Clone)", "");

        var result = obj.GetComponent<T>();
        result.canvas = canvas;
        result.canvas.sortingOrder = uiList.Count;

        return result;
    }

    public T Get<T>() where T : UIBase
    {
        string uiName = typeof(T).ToString();
        uiList.TryGetValue(uiName, out UIBase ui);
        if (ui == null)
        {
            return default;
        }

        return (T)ui;
    }

    public void Hide<T>()
    {
        string uiName = typeof(T).ToString();

        Hide(uiName);
    }

    public void Hide(string uiName)
    {
        uiList.TryGetValue(uiName, out UIBase ui);

        if (ui == null)
            return;

        Destroy(ui.canvas.gameObject);
        uiList.Remove(uiName);
    }
    
    void RemoveNull()
    {
        List<string> tempList = new List<string>(uiList.Count);
        foreach (var temp in uiList)
        {
            if (temp.Value == null)
                tempList.Add(temp.Key);
        }

        foreach (var temp in tempList)
        {
            uiList.Remove(temp);
        }
    }

    public void ToggleUI(UIBase ui)
    {
        ui.toggle = !ui.toggle;
        ui.gameObject.SetActive(ui.toggle);
        if (ui.toggle)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
