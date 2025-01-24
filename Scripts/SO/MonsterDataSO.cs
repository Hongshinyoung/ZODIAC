using UnityEngine;

[CreateAssetMenu(fileName = "MonsterDataSO", menuName = "ScriptableObjects/MonsterDataSO", order = 1)]
public class MonsterDataSO : ScriptableObject
{
    public string Id;
    public string Name;
    public float AttackRange;
    public float SkillRange;
    public float ForceTransitionTime;
    public float Force;
    public float SkillTransitionTime;
    public float DealingStartTransitionTime;
    public float DealingEndTransitionTime;
    public float DaseSpeed;
    public float BaseRotationDamping;
    public float WalkSpeedModifier;
    public float ChaseSpeedModifier;
    public float PlayerChasingRange;
    public float Hp;
    public float AttackPower;
}
