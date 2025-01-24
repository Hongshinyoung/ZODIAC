using System.Collections;
using TMPro;
using UnityEngine;

public class UIWelcome : UIBase
{
    public TMP_Text welcomeText;      // 환영 메시지 텍스트
    public CanvasGroup canvasGroup; // 페이드 효과용 캔버스 그룹

    public void SetWelcomeMessage()
    {
        welcomeText.text = $"환영합니다 {GameManager.Instance.UserData.name}님!";
        StartCoroutine(ExecuteWelcomeSequence());
    }

    private IEnumerator ExecuteWelcomeSequence()
    {
        yield return StartCoroutine(FadeIn());
        yield return new WaitForSeconds(2f);
        SceneLoadManager.Instance.ChangeScene("InGameTownScene", null, UnityEngine.SceneManagement.LoadSceneMode.Single);
        UIManager.Instance.Get<UIEquipment>().ToggleUI();
    }

    private IEnumerator FadeIn()
    {
        float time = 0;
        while (time < 1f)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, time);
            yield return null;
        }
        canvasGroup.alpha = 1;
    }
}
