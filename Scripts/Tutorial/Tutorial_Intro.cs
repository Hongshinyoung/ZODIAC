using System.Collections;
using UnityEngine;

public class Tutorial_Intro : TutorialBase
{
    private ClearData clearData;
    public override void Enter()
    {
        clearData = GameManager.Instance.clearData;
        if(clearData.stageCleared[0,0] == false)
        {
            popup = UIManager.Instance.Show<UIPopupTutorial>();
            popup.SetTutorialData(dialogs[curDialogIndex]);
            popup.OnPrintLastChar += PrintNextDialogORHideUI;
        }
    }

    public override void Execute(TutorialController controller)
    {
        if(isCompleted)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        popup.OnPrintLastChar -= PrintNextDialogORHideUI;
        Destroy(this.gameObject);
    }

    private void PrintNextDialogORHideUI(bool isDescribe)
    {
        if (isDescribe == false) // 대화가 다 출력 되었다면 
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
        isCompleted = true;
    }
}
