using System.Collections.Generic;

[System.Serializable]
public class MetaMonsterData
{
    public string Id; //MON2001
    public string Name;
    public string MonsterType;
    public float AttackRange;
    public float SkillRange;
    public float ForceTransitionTime;
    public float Force;
    public float SkillCoolTime;
    public float SkillTransitionTime;
    public float DealingStartTransitionTime;
    public float DealingEndTransitionTime;
    public float BaseSpeed;
    public float BaseRotationDamping;
    public float WalkSpeedModifier;
    public float ChaseSpeedModifier;
    public float PlayerChasingRange;
    public float Hp;
    public int AttackPower;
}

[System.Serializable]
public class MetaMonsterDataList
{
    public List<MetaMonsterData> MonsterData;
}

