using UnityEngine;

public class HealingSkill : SkillBase
{
    public GameObject effectPrefab;   
    public int healAmount = 20;    

    public int skillDelay = 10;
    public int mpCost = 20;

    public float lastActivatedTime = -Mathf.Infinity;

    public override void ActivateSkill(Vector3 targetPosition)
    {
        if (Time.time >= lastActivatedTime + skillDelay)
        {
            var playerData = GameManager.Instance.Player.Data;

            // 스킬 사용 조건 확인
            if (playerData.mp >= mpCost && playerData.hp < playerData.maxhp)
            {
                int actualHealAmount = Mathf.Min(healAmount, playerData.maxhp - playerData.hp);
                playerData.hp += actualHealAmount;
                playerData.mp -= mpCost;

                Vector3 playerPosition = GameManager.Instance.Player.transform.position;
                GameObject effect = Instantiate(effectPrefab, playerPosition, Quaternion.identity);
                Destroy(effect, 3f);

                lastActivatedTime = Time.time;
            }
            else
            {
                Debug.Log("Not enough MP or HP is already full.");
            }
        }
        else
        {
            Debug.Log("Skill is on cooldown. Please wait.");
        }
    }
}