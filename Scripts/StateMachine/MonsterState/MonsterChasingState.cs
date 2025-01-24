using UnityEngine;

public class MonsterChasingState : MonsterGroundState
{
    public MonsterChasingState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = groundData.ChaseSpeedModifier;
        base.Enter();
        StartAnimation(stateMachine.Monster.AnimationData.RunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Monster.AnimationData.RunParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (IsInSkillRange())
        {
            if (stateMachine.Monster.Data.Type == MonsterType.Boss && (stateMachine.Monster.skillCoolTime >= stateMachine.Monster.Data.SkillCoolTime))
            {
                stateMachine.ChangeState(stateMachine.SkillState);
                return;
            }
        }
        
        if (IsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.BaseAttackState);
            return;
        }

        if (!IsInChasingRange())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }

        Chase();
    }

    private void Chase()
    {
        Vector3 movementDirection = GetPlayerDirection(false);

        Rotate(movementDirection.normalized);
        Move(movementDirection);
    }
}
