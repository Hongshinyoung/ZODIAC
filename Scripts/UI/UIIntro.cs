using UnityEngine;

public class UIIntro : UIBase
{
    public CanvasGroup introCanvasGroup;
    public UILoginPopup loginPopupUI;


    public override void Opened(params object[] param)
    {
        base.Opened(param);
    }

    public void OnStartButtonClickAsync()
    {
        SoundManager.Instance.PlaySound("ChoiceSFX", 0.2f);
        UIManager.Instance.Show<UIChoice>();
    }
}