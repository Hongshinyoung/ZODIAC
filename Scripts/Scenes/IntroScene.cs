using UnityEngine;

public class IntroScene : SceneBase
{
    protected override void Awake()
    {
        base.Awake();
        SoundManager.Instance.PlaySound("IntroBGM", 0.4f, true);
        Cursor.lockState = CursorLockMode.None;
        UIManager.Instance.Show<UIIntro>();
    }
}