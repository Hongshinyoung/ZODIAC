using UnityEngine;


public class FindWayHelper : MonoBehaviour
{
    [SerializeField] private Transform[] target; // 목적지 오브젝트
    [SerializeField] private Transform arrow; // 화살표 오브젝트
    [SerializeField] private float arrivalThreshold = 3f;
    private int curTargetIndex = 0;
    private TutorialController controller;

    private void Start()
    {
        controller = GetComponentInParent<TutorialController>();
        curTargetIndex = controller.curIndex;
        GameObject player = GameObject.FindWithTag("Player");
        if(player != null)
        {
            transform.SetParent(player.transform);
        }
    }

    private void Update()
    {
        if (target == null || arrow == null) return;

        Transform curTarget = target[curTargetIndex];
        Vector3 dir = curTarget.position - arrow.position;

        dir.y = 0f;

        if(dir != Vector3.zero)
        {
            arrow.rotation = Quaternion.LookRotation(dir);
        }

        float distance = Vector3.Distance(arrow.position, curTarget.position);
        if (distance <= arrivalThreshold)
        {
            NextTarget();
        }
    }

    private void NextTarget()
    {
        curTargetIndex++;

        if (curTargetIndex >= target.Length)
        {
            enabled = false;
        }
    }
}
