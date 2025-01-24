using UnityEngine;


public abstract class UIBase : MonoBehaviour
{
    [HideInInspector]
    public Canvas canvas;
    public bool toggle = false;

    public virtual void Opened(params object[] param) { } 

    public virtual void Hide()
    {
        UIManager.Instance.Hide(gameObject.name);
    }

    public virtual void ToggleUI()
    {
        UIManager.Instance.ToggleUI(this);
    }
}
