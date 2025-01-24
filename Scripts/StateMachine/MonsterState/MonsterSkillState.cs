public class MonsterSkillState : MonsterBattleState
{
    public MonsterSkillState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Monster.skillCoolTime = 0;
        StartAnimation(stateMachine.Monster.AnimationData.SkillParameterHash);
        stateMachine.Monster.skill.gameObject.SetActive(true);
        stateMachine.Monster.skill.ResetSkill();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Monster.AnimationData.SkillParameterHash);
    }

    public override void Update()
    {
        LookPlayer();

        float normalizedTime = GetNormalizedTime(stateMachine.Monster.Animator, "Skill");
        if (stateMachine.Monster.Data.SkillTransitionTime < normalizedTime)
        {
            if (IsInAttackRange())
            {
                stateMachine.ChangeState(stateMachine.BaseAttackState);
                return;
            }
            else if (IsInChasingRange())
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }
        }
    }
}
