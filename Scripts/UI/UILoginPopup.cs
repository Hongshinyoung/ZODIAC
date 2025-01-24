using UnityEngine;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;
using TMPro;

public class UILoginPopup : UIBase
{
    public TMP_InputField nameInputField;  // 이름 입력 필드
    public TMP_Text errorText;            // 에러 메시지
    public CanvasGroup popupCanvasGroup; // 페이드 효과용 캔버스 그룹
    public UIWelcome uiWelcome;
    public SaveData saveData;

    private const string NicknameKey = "PlayerNickname";

    public override void Opened(params object[] param)
    {
        base.Opened(param);
        popupCanvasGroup.alpha = 0;
        StartCoroutine(FadeInPopup());
    }

    private IEnumerator FadeInPopup()
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

    private void OnConfirmButtonClick() // 확인 버튼
    {
        SoundManager.Instance.PlaySound("ChoiceSFX", 0.2f);
        string nickname = nameInputField.text;

        if (string.IsNullOrWhiteSpace(nickname))
        {
            ShowError("이름은 비워둘 수 없습니다.");
        }
        else if (ContainsWhitespace(nickname))
        {
            ShowError("띄워쓰기가 허용되지 않습니다.");
        }
        else if (!IsValidNickname(nickname))
        {
            ShowError("이름은 한글, 영어, 숫자만 가능합니다!");
        }
        else if (nickname.Length > 8)
        {
            ShowError("8자리까지 가능합니다");
        }
        else
        {
            PlayerPrefs.SetString(NicknameKey, nickname);
            PlayerPrefs.Save();

            // 닉네임 확인 후 환영 메시지 표시
            ShowWelcomeMessage(nickname);
        }
    }

    private bool IsValidNickname(string nickname)
    {
        // 한글, 영어, 숫자만 허용
        return Regex.IsMatch(nickname, "^[a-zA-Z0-9가-힣]+$");
    }

    private bool ContainsWhitespace(string input)
    {
        // 공백 포함 여부 검사
        return input.Contains(" ");
    }

    private void ShowError(string message)
    {
        errorText.text = message;
        errorText.gameObject.SetActive(true);
    }

    private async void ShowWelcomeMessage(string nickname)
    {
        GameManager.Instance.UserData.name = nickname;
        var welcomeUI = UIManager.Instance.Show<UIWelcome>();
        welcomeUI.SetWelcomeMessage();
        GameManager.Instance.SaveGame();

        // 2초 후 게임 씬으로 이동
        await Task.Delay(500);
    }
}