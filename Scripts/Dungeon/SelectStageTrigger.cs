using System.Collections;
using UnityEngine;

public class SelectStageTrigger : ActivePlayerTrigger
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (IsCollisionWithLayer(other.gameObject, playerLayer))
        {
            UIManager.Instance.Show<UIPopupSelectStar>();
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(IsCollisionWithLayer(other.gameObject, playerLayer))
        {
            UIManager.Instance.Hide<UIPopupSelectStar>();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    IEnumerator StopTime()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }
}
