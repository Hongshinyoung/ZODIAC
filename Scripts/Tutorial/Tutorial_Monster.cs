using System.Collections;
using UnityEngine;

public class Tutorial_Monster : TutorialBase
{
    public override void Enter()
    {
        popup = UIManager.Instance.Show<UIPopupTutorial>();
        popup.OnPrintLastChar += PrintNextDialogORHideUI;
        popup.gameObject.SetActive(false);
        isCompleted = false;
        popup.SetTutorialData(dialogs[curDialogIndex]);
    }

    public override void Execute(TutorialController controller)
    {
        if (isCompleted == true)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        if (popup != null)
        {
            popup.OnPrintLastChar -= PrintNextDialogORHideUI;
        }
    }

    private void PrintNextDialogORHideUI(bool istyping)
    {
        if (istyping == false) // 대화가 다 출력 되었다면 
        {
            curDialogIndex++;
            if (curDialogIndex < dialogs.Count) // 다음 대화 출력
            {
                StartCoroutine(DelayPrint(1.5f));
            }
            else // 대화 종료 ui 닫기
            {
                StartCoroutine(DeleyCloseUI(2f));
            }
        }
    }

    IEnumerator DelayPrint(float delay)
    {
        yield return new WaitForSeconds(delay);
        popup.SetTutorialData(dialogs[curDialogIndex]);
    }

    IEnumerator DeleyCloseUI(float delay)
    {
        yield return new WaitForSeconds(delay);
        popup.gameObject.SetActive(false);
    }
}
