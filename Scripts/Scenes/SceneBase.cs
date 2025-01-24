using UnityEngine;

public abstract class SceneBase : MonoBehaviour
{
    public Transform uiTransform;

    protected virtual void Awake()
    {
        UIManager.UITransform = uiTransform;

        if (!SceneLoadManager.Instance.isDontDestroy)
        {
            SceneLoadManager.Instance.ChangeScene("DontDestroy", () =>
            {   SceneLoadManager.Instance.isDontDestroy = true;
                SceneLoadManager.Instance.UnLoadScene("DontDestroy");
            }, UnityEngine.SceneManagement.LoadSceneMode.Additive,false);
        }
    }

    protected virtual void OnDestroy()
    {
        uiTransform = null;
    }
}
