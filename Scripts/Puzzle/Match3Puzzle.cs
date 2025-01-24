using System.Collections.Generic;
using UnityEngine;

public class Match3Puzzle : Puzzle
{
    /* 같은 색상의 퍼즐이 3개 이상 닿으면 파괴하는 클래스 */

    private List<Puzzle> collidingPuzzles = new List<Puzzle>();
    [SerializeField] private GameObject door;

    // 퍼즐 레이어가 3개이상 충돌하면 파괴
    protected override void OnCollisionEnter(Collision collision)
    {
        if (IsCollisionWithLayer(collision.gameObject, puzzleLayer))
        {
            Puzzle otherPuzzle = collision.gameObject.GetComponent<Puzzle>();
            //충돌한 퍼즐이 이미 리스트에 추가되었는지 검사, 색상비교
            if (IsSameColor(otherPuzzle) && !collidingPuzzles.Contains(otherPuzzle))
            {
                collidingPuzzles.Add(otherPuzzle);

                if (collidingPuzzles.Count >= 2) //자기자신은 제외한 수
                {
                    collidingPuzzles.Add(this); //자기 자신도 포함
                    PlayDestroyEffect(transform.position);
                    SoundManager.Instance.PlaySound("3matchSFX", 0.2f);
                    GameManager.Instance.PuzzleManager.DestroyPuzzles(collidingPuzzles);
                    if (door != null) door.SetActive(true);
                    IsComplete = true; //완료체크
                }
            }
        }
    }

    protected override void OnCollisionExit(Collision collision)
    {
        Puzzle otherPuzzle = collision.gameObject.GetComponent<Puzzle>();
        if (otherPuzzle != null && collidingPuzzles.Contains(otherPuzzle))
        {
            collidingPuzzles.Remove(otherPuzzle);
        }
    }
}
