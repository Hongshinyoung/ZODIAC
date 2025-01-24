using UnityEngine;

public class LaserPuzzle : Puzzle
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float maxDistance = 350f;
    [SerializeField] private int maxReflections = 7;
    [SerializeField] private LayerMask correctLaserLayer;// 최종적으로 닿으면 클리어되는 레이어
    [SerializeField] private LayerMask mirrorLayer;// 거울 레이어
    [SerializeField] private Transform startPos;
    [SerializeField] private GameObject teleportObj;

    private void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    private void Update()
    {
        FireLaser();
    }

    private void FireLaser()
    {
        lineRenderer.positionCount = 1; // 시작점만 설정
        lineRenderer.SetPosition(0, startPos.position);

        lineRenderer.startWidth = 1.5f;
        lineRenderer.endWidth = 1.5f;

        // 반사 처리
        ReflectLaser(transform.position, transform.forward, 0);
    }

    private void ReflectLaser(Vector3 origin, Vector3 direction, int currentReflection)
    {
        if (currentReflection >= maxReflections)
            return;

        RaycastHit hit;

        // 충돌 체크
        if (Physics.Raycast(origin, direction, out hit, maxDistance))
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(currentReflection + 1, hit.point);

            // 거울에 닿으면 반사 처리
            if (IsCollisionWithLayer(hit.collider.gameObject, mirrorLayer))
            {
                Vector3 reflectDirection = Vector3.Reflect(direction, hit.normal);
                ReflectLaser(hit.point, reflectDirection, currentReflection + 1);
            }
            // 퍼즐 오브젝트에 닿으면
            else if (IsCollisionWithLayer(hit.collider.gameObject, correctLaserLayer)) //추후에 거울, 최종 나누기
            {
                IsComplete = true; // 완료 체크
                var particle = hit.collider.GetComponentInChildren<ParticleSystem>(true);
                if(particle != null)
                {
                    particle.gameObject.SetActive(true);
                }
                teleportObj.SetActive(true);
            }
        }
        else
        {
            // 충돌하지 않으면 최대 거리로 설정
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(currentReflection + 1, origin + direction * maxDistance);
        }
    }
}
