using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSkill : SkillBase
{
    public GameObject effectPrefab;  // 효과를 나타낼 프리팹
    public GameObject meteorPrefab; // 실제 Meteor 오브젝트

    public float fallSpeed = 100f;    // 메테오가 떨어지는 속도
    public float heightOffset = 15f; // 메테오 시작 높이
    public int skillDelay = 15;
    public int mpCost = 30;
    public int damage;             // 메테오 데미지

    public float spawnDuration = 3f; // 메테오 생성 지속 시간
    public float spawnInterval = 1f; // 메테오 생성 간격
    public float spawnRadius = 10f;  // 메테오 생성 반경

    public float lastActivatedTime = -Mathf.Infinity;

    private List<GameObject> activeMeteors = new List<GameObject>(); // 활성화된 메테오 관리 리스트

    public override void ActivateSkill(Vector3 targetPosition)
    {
        if (Time.time >= lastActivatedTime + skillDelay)
        {
            if (GameManager.Instance.Player.Data.mp >= mpCost)
            {
                GameObject effect = Instantiate(effectPrefab, targetPosition, Quaternion.identity);
                Destroy(effect, spawnDuration);

                StartCoroutine(SpawnMeteors(targetPosition))
                    ;
                GameManager.Instance.Player.Data.mp -= mpCost;
                lastActivatedTime = Time.time;
            }
        }
        else 
        {
            Debug.Log("Skill is on cooldown. Please wait.");
        }

    }

    private IEnumerator SpawnMeteors(Vector3 targetPosition)
    {
        float startTime = Time.time; // 시작 시간 기록

        while (Time.time - startTime < spawnDuration)
        {
            Vector3 randomPosition = GetRandomPositionInArea(targetPosition, spawnRadius);
            Vector3 startPosition = new Vector3(randomPosition.x, randomPosition.y + heightOffset, randomPosition.z);

            GameObject meteor = Instantiate(meteorPrefab, startPosition, Quaternion.identity);
            activeMeteors.Add(meteor); 

            Meteor meteorScript = meteor.GetComponent<Meteor>();
            if (meteorScript != null)
            {
                meteorScript.Initialize(randomPosition, fallSpeed, damage);
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        DestroyAllMeteors();
    }

    private Vector3 GetRandomPositionInArea(Vector3 center, float radius)
    {
        float randomAngle = Random.Range(0f, Mathf.PI * 2f);
        float randomRadius = Random.Range(0f, radius);
        float xOffset = Mathf.Cos(randomAngle) * randomRadius;
        float zOffset = Mathf.Sin(randomAngle) * randomRadius;

        return new Vector3(center.x + xOffset, center.y, center.z + zOffset);
    }

    private void DestroyAllMeteors()
    {
        foreach (GameObject meteor in activeMeteors)
        {
            if (meteor != null)
            {
                Destroy(meteor); 
            }
        }
        activeMeteors.Clear();
    }
}