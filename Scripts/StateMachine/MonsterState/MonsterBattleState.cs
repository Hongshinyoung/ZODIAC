using UnityEngine;

public class MonsterBattleState : MonsterBaseState
{
    private bool alreadyAppliedForce;

    public MonsterBattleState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0;
        base.Enter();
        StartAnimation(stateMachine.Monster.AnimationData.BattleParameterHash);
        alreadyAppliedForce = false;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Monster.AnimationData.BattleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        LookPlayer();

        if (IsInSkillRange())
        {
            if ((stateMachine.Monster.Data.Type == MonsterType.Boss) && (stateMachine.Monster.skillCoolTime >= stateMachine.Monster.Data.SkillCoolTime))
            {
                stateMachine.ChangeState(stateMachine.SkillState);
                return;
            }
        }
        
        if(stateMachine.currentState != stateMachine.BaseAttackState)
        {
            stateMachine.ChangeState(stateMachine.BaseAttackState);
        }
    }

    protected void TryApplyForce()
    {
        if (alreadyAppliedForce) return;

        alreadyAppliedForce = true;

        stateMachine.Monster.ForceReceiver.Reset();

        stateMachine.Monster.ForceReceiver.AddForce(GetPlayerDirection() * stateMachine.Monster.Data.Force);
    }

    protected void ForceMove()
    {
        stateMachine.Monster.Controller.Move(stateMachine.Monster.ForceReceiver.Movement * Time.deltaTime);
    }
}
