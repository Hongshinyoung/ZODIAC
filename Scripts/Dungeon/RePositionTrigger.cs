using UnityEngine;

public class RePositionTrigger : ActivePlayerTrigger
{
    // 플레이어 ,몬스터, 퍼즐 각각에 원래 위치 정보(상태를 초기화 해주는 메소드를 가지고 있고) 여기서는 레이어 체크 후 호출하기만

    [SerializeField] private LayerMask monsterLayer;
    [SerializeField] private LayerMask puzzleLayer;
    private MapData mapData;
    private DungeonManager dungeonManager;

    private void Awake()
    {
        mapData = GetComponentInParent<MapData>();
        dungeonManager = GameManager.Instance.DungeonManager;
    }

    protected override void OnTriggerEnter(Collider other) // 플레이어, 몬스터
    {
        if(IsCollisionWithLayer(other.gameObject, playerLayer)) // 플레이어면
        {
            other.gameObject.transform.position = new Vector3(0, 10, 0);
        }

        else if(IsCollisionWithLayer(other.gameObject, monsterLayer)) // 몬스터면
        {
            if(dungeonManager != null && dungeonManager.InitialMonsterPositions.ContainsKey(other.gameObject))
            {
                Vector3 initialPosition = dungeonManager.InitialMonsterPositions[other.gameObject];

                CharacterController characterController = other.GetComponent<CharacterController>();
                if (characterController != null)
                {
                    characterController.enabled = false;
                    characterController.transform.position = initialPosition;
                    characterController.enabled = true;
                }
            }
        }
        else if(IsCollisionWithLayer(other.gameObject, puzzleLayer))
        {
            Puzzle puzzle = other.GetComponent<Puzzle>();
            if (puzzle != null)
            {
                other.gameObject.transform.position = puzzle.nativePosition;
            }
        }
    }
}
