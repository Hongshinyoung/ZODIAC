using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterData : DataBase<MetaMonsterData>
{
    public string Id; //MON2001
    public string Name;
    public MonsterType Type;
    public float AttackRange;
    public float SkillRange;
    public float ForceTransitionTime;
    public float Force;
    public float SkillCoolTime;
    public float SkillTransitionTime;
    public float DealingStartTransitionTime;
    public float DealingEndTransitionTime;
    public MonsterGroundData GroundData;
    public CharacterStat Stat;

    public override void SetData(MetaMonsterData metaData)
    {
        Id = metaData.Id;
        Name = metaData.Name;
        if(Enum.TryParse(metaData.MonsterType, out Type))
        {
            Debug.Log("몬스터 타입 문자열에서 enum 변환 성공");
        }
        else
        {
            Debug.Log("몬스터 타입 문자열에서 enum 변환 실패");
        }
        AttackRange = metaData.AttackRange;
        SkillRange = metaData.SkillRange;
        ForceTransitionTime = metaData.ForceTransitionTime;
        Force = metaData.Force;
        SkillCoolTime = metaData.SkillCoolTime;
        SkillTransitionTime = metaData.SkillTransitionTime;
        DealingEndTransitionTime = metaData.DealingEndTransitionTime;
        DealingStartTransitionTime = metaData.DealingStartTransitionTime;
        GroundData = new MonsterGroundData();
        GroundData.BaseSpeed = metaData.BaseSpeed;
        GroundData.BaseRotationDamping = metaData.BaseRotationDamping;
        GroundData.WalkSpeedModifier = metaData.WalkSpeedModifier;
        GroundData.ChaseSpeedModifier = metaData.ChaseSpeedModifier;
        GroundData.PlayerChasingRange = metaData.PlayerChasingRange;
        Stat = new CharacterStat();
        Stat.Hp = metaData.Hp;
        Stat.AttackPower = metaData.AttackPower;
    }
}


[System.Serializable]
public class MonsterDataList : DataBaseList<string, MonsterData, MetaMonsterData>
{
    public override void SetData(List<MetaMonsterData> DataList)
    {
        datas = new Dictionary<string, MonsterData>(DataList.Count);

        DataList.ForEach(obj =>
        {
            MonsterData monster = new MonsterData();
            monster.SetData(obj);
            datas.Add(monster.Id, monster);
        });
    }
}
