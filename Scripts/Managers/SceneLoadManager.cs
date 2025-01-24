using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Threading.Tasks;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;
    
    public bool isDontDestroy = false;
    public string nowSceneName = "";

    public UILoading uiLoading;

    protected override void Awake()
    {
        base.Awake();
        nowSceneName = SceneManager.GetActiveScene().name;
        uiLoading = UIManager.Instance.Show<UILoading>();
        var canvas = uiLoading.transform.parent;
        canvas.GetComponent<Canvas>().sortingOrder = 100;
        canvas.SetParent(transform);
    }

    public async void ChangeScene(string SceneName, Action callback = null, LoadSceneMode loadSceneMode = LoadSceneMode.Single, bool firstScene = true)
    {
        if (firstScene) { await uiLoading.FadeIn(); }
        
        var operation = SceneManager.LoadSceneAsync(SceneName, loadSceneMode);

        while (!operation.isDone) 
        {
            await Task.Yield();
        }

        if (loadSceneMode == LoadSceneMode.Single)
            nowSceneName = SceneName;

        callback?.Invoke();
        
        if (firstScene) { await uiLoading.FadeOut(); }
    }

    public async void UnLoadScene(string SceneName, Action callback = null) 
    {
        var operation = SceneManager.UnloadSceneAsync(SceneName);

        while (!operation.isDone) { await Task.Yield(); }
        callback?.Invoke();
    }
}