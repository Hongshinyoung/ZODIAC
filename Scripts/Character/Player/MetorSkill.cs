using UnityEngine;

public class MeteorSkill : SkillBase
{
    public GameObject effectPrefab;
    public GameObject meteorPrefab; // 실제 Meteor 오브젝트

    public float fallSpeed = 100f;   // 떨어지는 속도
    public float heightOffset = 15f; // 시작 높이
    public int skillDelay = 10;
    public int mpCost = 20;
    public int damage;

    public float lastActivatedTime = -Mathf.Infinity;

    public override void ActivateSkill(Vector3 targetPosition)
    {
        if (Time.time >= lastActivatedTime + skillDelay)
        {
            if (GameManager.Instance.Player.Data.mp >= mpCost)
            {
                Vector3 startPosition = new Vector3(targetPosition.x, targetPosition.y + heightOffset, targetPosition.z);
                GameObject meteor = Instantiate(meteorPrefab, startPosition, Quaternion.identity);

                GameObject effect = Instantiate(effectPrefab, targetPosition, Quaternion.identity);
                Destroy(effect, 3f);

                Meteor meteorScript = meteor.GetComponent<Meteor>();
                if (meteorScript != null)
                {
                    meteorScript.Initialize(targetPosition, fallSpeed, damage);
                }

                GameManager.Instance.Player.Data.mp -= mpCost;

                lastActivatedTime = Time.time;
            }
            else
            {
                Debug.Log("Not enough MP to cast the skill.");
            }
        }
        else
        {
            Debug.Log("Skill is on cooldown. Please wait.");
        }
    }
}
