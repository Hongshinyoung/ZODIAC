using UnityEngine;

public class DestinationPuzzle : Puzzle
{
    /* 퍼즐이 목적지에 도착하면 완료 클래스 */

    [SerializeField] private Transform correctPos; // 퍼즐 목적지
    [SerializeField] private LayerMask correctPuzzle;
    [SerializeField] private Transform bossSpawnPos;
    private GameObject door;

    private bool isCorrectPos = false; //옳바른 위치인지


    protected override void OnTriggerEnter(Collider other)
    {
        if (IsCollisionWithLayer(other.gameObject, correctPuzzle))
        {
            isCorrectPos = true;
            IsComplete = true; //완료체크
            Destroy(other.gameObject, 1f);
            Destroy(this.gameObject, 1f);
            if (bossSpawnPos != null)
            {
                bossSpawnPos.gameObject.SetActive(true);
                SoundManager.Instance.PlaySound("BossSFX", 0.2f, true);
            }
            if(door != null) door.gameObject.SetActive(true);
            Debug.Log("문생성");
        }
    }


    protected override void OnTriggerExit(Collider other)
    {
        if (IsCollisionWithLayer(other.gameObject, correctPuzzle))
        {
            isCorrectPos = false;
            IsComplete = false; //떨어지면 다시 완료 해제
        }
    }
}
