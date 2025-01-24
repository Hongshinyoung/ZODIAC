using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIPopupTutorial : UIBase
{
    public TMP_Text tutorialText;
    public event Action<bool> OnPrintLastChar;

    public void SetTutorialData(string text) // ui에서 보여줄 데이터 세팅 
    {
        gameObject.SetActive(true);
        tutorialText.text = text;
        StartCoroutine(ShowTutorialText(text, 0.05f));
    }

    IEnumerator ShowTutorialText(string text, float delay) // 한 글자씩 출력하기 
    {
        string currentText = "";
        bool isDescribe;
        foreach (char c in text)
        {
            isDescribe = true;
            currentText += c;
            tutorialText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
        isDescribe = false;
        OnPrintLastChar?.Invoke(isDescribe);
    }
}
