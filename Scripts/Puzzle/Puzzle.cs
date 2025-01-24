using UnityEngine;

public enum PuzzleType
{
    Match3,
    Destination,
    Laser
}

public abstract class Puzzle : MonoBehaviour
{
    //퍼즐 공통 정보
    [SerializeField] protected LayerMask puzzleLayer;
    [SerializeField] protected Color puzzleColor;
    [SerializeField] private PuzzleType puzzleType;
    [SerializeField] private ParticleSystem effect;
    public Vector3 nativePosition;

    public PuzzleType Type => puzzleType;

    private bool isCompleted = false;
    public bool IsComplete
    {
        get => isCompleted;
        protected set
        {
            isCompleted = value;
        }
    }


    protected virtual void Start()
    {
        nativePosition = transform.position;
        GameManager.Instance.PuzzleManager?.RegisterPuzzle(this);
    }


    protected virtual void OnDestroy()
    {
        GameManager.Instance.PuzzleManager?.UnregisterPuzzle(this);
    }

    protected void PlayDestroyEffect(Vector3 position)
    {
        effect.Play();
        Destroy(effect.gameObject, 1f); // 2초 뒤 이펙트 자동 제거

    }

    /* 공통함수 */
    //특정 레이어와 충돌 검사
    protected bool IsCollisionWithLayer(GameObject obj, LayerMask layerMask)
    {
        return ((1 << obj.layer) & layerMask) != 0;
    }


    //다른 퍼즐과 색상 비교
    protected bool IsSameColor(Puzzle otherPuzzle)
    {
        return otherPuzzle != null && otherPuzzle.puzzleColor == puzzleColor;
    }


    protected virtual void OnCollisionEnter(Collision collision) { }
    protected virtual void OnCollisionExit(Collision collision) { }
    protected virtual void OnTriggerEnter(Collider other) { }
    protected virtual void OnTriggerExit(Collider other) { }

}
