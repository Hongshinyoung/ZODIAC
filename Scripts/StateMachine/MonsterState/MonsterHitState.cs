using UnityEngine;

public class MonsterHitState : MonsterBaseState
{
    private float hitTime = 0;
    public MonsterHitState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Monster.AnimationData.HitParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Monster.AnimationData.HitParameterHash);
    }

    public override void Update()
    {
        if(hitTime > 1f)
        {
            if (stateMachine.Monster.Hp <= 0)
            {
                stateMachine.ChangeState(stateMachine.DieState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
                hitTime = 0f;
            }
        }
        hitTime += Time.deltaTime;
        
    }
}
