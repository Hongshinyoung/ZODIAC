using System;
using UnityEngine;

[Serializable]
public class MonsterAnimationData
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string runParameterName = "Run";

    [SerializeField] private string battleParameterName = "@Battle";
    [SerializeField] private string baseAttackParameterName = "BaseAttack";
    [SerializeField] private string skillParameterName = "Skill";

    [SerializeField] private string hitParameterName = "@Hit";
    [SerializeField] private string dieParameterName = "Die";

    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }
    public int BattleParameterHash { get; private set; }
    public int BaseAttackParameterHash { get; private set; }
    public int SkillParameterHash { get; private set; }
    public int HitParameterHash { get; private set; }
    public int DieParameterName { get; private set; }


    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);
        BattleParameterHash = Animator.StringToHash(battleParameterName);
        BaseAttackParameterHash = Animator.StringToHash(baseAttackParameterName);
        SkillParameterHash = Animator.StringToHash(skillParameterName);
        HitParameterHash = Animator.StringToHash(hitParameterName);
        DieParameterName = Animator.StringToHash(dieParameterName);
    }
}
