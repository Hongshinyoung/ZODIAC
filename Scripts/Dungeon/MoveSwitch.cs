using System.Collections;
using UnityEngine;

public class MoveSwitch : ActivePlayerTrigger
{
    [SerializeField] private GameObject[] movingFloor;
    private float moveDistance = 8f;
    private float moveSpeed = 8f;

    private Vector3 initialPosition0;
    private Vector3 targetPosition0;
    private Vector3 initialPosition1;
    private Vector3 targetPosition1;
    private Vector3 secondTargetPosition1; // 두 번째 발판의 추가 목표 위치

    private int playerORPuzzleOnCountO = 0; // 플레이어가 밟고 있는 발판의 개수

    private Coroutine moveCoroutine;

    private void Start()
    {
        if (movingFloor == null || movingFloor.Length < 2) return;

        // 초기 위치 저장 및 목표 지점 저장 
        initialPosition0 = movingFloor[0].transform.position;
        targetPosition0 = initialPosition0 + transform.forward * moveDistance;

        initialPosition1 = movingFloor[1].transform.position;
        targetPosition1 = initialPosition1 + transform.forward * moveDistance;
        secondTargetPosition1 = targetPosition1 + transform.forward * moveDistance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsCollisionWithLayer(collision.gameObject, playerLayer))
        {
            playerORPuzzleOnCountO++;
            StartMove();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (IsCollisionWithLayer(collision.gameObject, playerLayer))
        {
            playerORPuzzleOnCountO--;
            StartMove();
        }
        else if (playerORPuzzleOnCountO <= 0)
        {
            StartMove();
        }
    }

    private void StartMove()
    {
        if (moveCoroutine != null) // 실행중이면 코루틴 종료 
        {
            StopCoroutine(moveCoroutine);
        }

        if (playerORPuzzleOnCountO == 0)
        {
            moveCoroutine = StartCoroutine(MoveFloor(initialPosition0, initialPosition1));
        }
        else if (playerORPuzzleOnCountO == 1)
        {
            moveCoroutine = StartCoroutine(MoveFloor(targetPosition0, targetPosition1));
        }
        else if (playerORPuzzleOnCountO == 2)
        {
            moveCoroutine = StartCoroutine(MoveFloor(targetPosition0, secondTargetPosition1));
        }
    }

    private IEnumerator MoveFloor(Vector3 target0, Vector3 target1)
    {
        Vector3 startPosition0 = movingFloor[0].transform.position;
        Vector3 startPosition1 = movingFloor[1].transform.position;

        float moveDistance0 = Vector3.Distance(startPosition0, target0);
        float moveDistance1 = Vector3.Distance(startPosition1, target1);

        // 이동 거리가 0이면 코루틴 종료
        if (moveDistance0 == 0 && moveDistance1 == 0)
        {
            yield break;
        }

        float startTime = Time.time;

        while (true)
        {
            // 경과 시간 계산
            float elapsedTime = (Time.time - startTime) * moveSpeed;

            // 진행 비율 계산 (0~1 사이 값으로 제한)
            float progress0 = moveDistance0 > 0 ? Mathf.Clamp01(elapsedTime / moveDistance0) : 1f;
            float progress1 = moveDistance1 > 0 ? Mathf.Clamp01(elapsedTime / moveDistance1) : 1f;

            // 이동
            movingFloor[0].transform.position = Vector3.Lerp(startPosition0, target0, progress0);
            movingFloor[1].transform.position = Vector3.Lerp(startPosition1, target1, progress1);

            // 두 발판이 목표 지점에 도달했는지 확인
            if (progress0 >= 1f && progress1 >= 1f)
            {
                break;
            }

            yield return null;
        }

        // 최종 위치로 보정
        movingFloor[0].transform.position = target0;
        movingFloor[1].transform.position = target1;

        moveCoroutine = null;
    }
}   