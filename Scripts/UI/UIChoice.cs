using System.Collections;
using UnityEngine;

public class UIChoice : UIBase
{
    public CanvasGroup popupCanvasGroup; // 페이드 효과용 캔버스 그룹


    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void OnGameStartButtonClick()
    {
        // 환영 메시지 UI 표시
        SoundManager.Instance.PlaySound("ChoiceSFX", 0.2f);
        GameManager.Instance.LoadGame();
        var welcomeUI = UIManager.Instance.Show<UIWelcome>();
        welcomeUI.SetWelcomeMessage();
    }

    public void OnNewStartButtonClick()
    {
        SoundManager.Instance.PlaySound("ChoiceSFX", 0.2f);
        UIManager.Instance.Show<UILoginPopup>();
    }

    private IEnumerator FadeIn()
    {
        float time = 0;
        while (time < 1f)
        {
            time += Time.deltaTime;
            popupCanvasGroup.alpha = Mathf.Lerp(0, 1, time);
            yield return null;
        }
        popupCanvasGroup.alpha = 1;
    }
}