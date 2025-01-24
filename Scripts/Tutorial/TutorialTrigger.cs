using System.Collections;
using UnityEngine;

public class TutorialTrigger : TutorialBase
{
    private bool isDescribe;
    private void OnTriggerEnter(Collider other)
    {
        if(IsCollisionWithLayer(other.gameObject))
        {
            popup.SetTutorialData(dialogs[curDialogIndex]);
        }
    }

    public override void Enter()
    {
        popup = UIManager.Instance.Show<UIPopupTutorial>();
        popup.OnPrintLastChar += PrintNextDialogORHideUI;
        popup.gameObject.SetActive(false);
        isCompleted = false;
    }

    private void PrintNextDialogORHideUI(bool isTyping)
    {
        if (isTyping == false)
        {
            isDescribe = true;
            curDialogIndex++;
            if(curDialogIndex < dialogs.Count)
            {
                StartCoroutine(DelayPrint(1.5f));
            }
            else
            {
                StartCoroutine(DelayCloseUI(2f));
            }
        }
        isDescribe = true;

    }

    IEnumerator DelayPrint(float delay)
    {
        yield return new WaitForSeconds(delay);
        popup.SetTutorialData(dialogs[curDialogIndex]);
    }

    IEnumerator DelayCloseUI(float delay)
    {
        yield return new WaitForSeconds(delay);
        popup.gameObject.SetActive(false);
        isDescribe = false;
    }

    public override void Execute(TutorialController controller)
    {
        if (GameManager.Instance.PuzzleManager.AreAllPuzzlesCompleted())
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        if(popup != null)
        {
            popup.gameObject.SetActive(false);
            popup.OnPrintLastChar -= PrintNextDialogORHideUI;
        }
        Destroy(this.gameObject);
    }
}
