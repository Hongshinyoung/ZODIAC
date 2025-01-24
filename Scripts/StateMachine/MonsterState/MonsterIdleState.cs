using UnityEngine;

public class MonsterIdleState : MonsterGroundState
{
    private float nonWalkingTime = 0f;

    public MonsterIdleState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f;

        base.Enter();
        StartAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (IsInChasingRange())
        {
            nonWalkingTime = 0f;
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }
        else if (nonWalkingTime > 2f)
        {
            nonWalkingTime = 0f;
            stateMachine.ChangeState(stateMachine.WalkState);
            return;
        }

        nonWalkingTime += Time.deltaTime;
    }
}
