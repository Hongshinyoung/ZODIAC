using UnityEngine;

[SerializeField]
public class MonsterStat
{
    [field: SerializeField] public string Id;
    [field: SerializeField] public string Name;
    [field: SerializeField] public MonsterType Type;
    [field: SerializeField] public float AttackRange;
    [field: SerializeField] public float SkillRange;
    [field: SerializeField][field: Range(0f, 3f)] public float ForceTransitionTime;
    [field: SerializeField][field: Range(-10f, 10f)] public float Force;
    [field: SerializeField] public float SkillCoolTime;
    [field: SerializeField] public float SkillTransitionTime;
    [field: SerializeField][field: Range(0f, 1f)] public float DealingStartTransitionTime;
    [field: SerializeField][field: Range(0f, 1f)] public float DealingEndTransitionTime;
    [field: SerializeField] public MonsterGroundData GroundData;
    [field: SerializeField] public CharacterStat Stat;

    public MonsterStat(MonsterData monsterData)
    {
        Id = monsterData.Id;
        Name = monsterData.Name;
        Type = monsterData.Type;
        AttackRange = monsterData.AttackRange;
        SkillRange = monsterData.SkillRange;
        ForceTransitionTime = monsterData.ForceTransitionTime;
        Force = monsterData.Force;
        SkillCoolTime = monsterData.SkillCoolTime;
        SkillTransitionTime = monsterData.SkillTransitionTime;
        DealingStartTransitionTime = monsterData.DealingStartTransitionTime;
        DealingEndTransitionTime = monsterData.DealingEndTransitionTime;
        GroundData = new MonsterGroundData();
        GroundData.BaseSpeed = monsterData.GroundData.BaseSpeed;
        GroundData.BaseRotationDamping = monsterData.GroundData.BaseRotationDamping;
        GroundData.WalkSpeedModifier = monsterData.GroundData.WalkSpeedModifier;
        GroundData.ChaseSpeedModifier = monsterData.GroundData.ChaseSpeedModifier;
        GroundData.PlayerChasingRange = monsterData.GroundData.PlayerChasingRange;
        Stat = new CharacterStat();
        Stat.Hp = monsterData.Stat.Hp;
        Stat.AttackPower = monsterData.Stat.AttackPower;
    }
}
