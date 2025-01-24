public class MonsterBaseAttackState : MonsterBattleState
{
    public MonsterBaseAttackState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Monster.ToggleNormalAttackParts();
        StartAnimation(stateMachine.Monster.AnimationData.BaseAttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.Monster.ToggleNormalAttackParts();
        StopAnimation(stateMachine.Monster.AnimationData.BaseAttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.Monster.Animator, "BaseAttack");
        if (normalizedTime < stateMachine.Monster.Data.ForceTransitionTime)
        {
            TryApplyForce();
        }
        else
        {
            if(IsInAttackRange())
            {
                stateMachine.ChangeState(stateMachine.BattleState);
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
