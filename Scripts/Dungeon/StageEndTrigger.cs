using UnityEngine;

public class StageEndTrigger : ActivePlayerTrigger
{
    protected override void OnTriggerStay(Collider other)
    {
        if (IsCollisionWithLayer(other.gameObject, playerLayer))
        {
            if (GameManager.Instance.PuzzleManager.AreAllPuzzlesCompleted())
            {
                GameManager.Instance.DungeonManager.StageClear();
            }
        }
    }
}
