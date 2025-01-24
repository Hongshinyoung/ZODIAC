using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialBase : MonoBehaviour
{
    protected UIPopupTutorial popup;
    [SerializeField]protected List<string> dialogs;
    [SerializeField] protected LayerMask playerLayer;
    protected bool isCompleted = false;
    protected int curDialogIndex = 0;

    public abstract void Enter();
    public abstract void Execute(TutorialController controller);
    public abstract void Exit();

    protected bool IsCollisionWithLayer(GameObject obj)
    {
        return ((1 << obj.layer) & playerLayer) != 0;
    }

}
