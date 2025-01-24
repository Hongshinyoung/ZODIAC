using UnityEngine;

public class MonsterWalkState : MonsterGroundState
{
    float walkingTime = 0;
    float walkingDuration = 0f;
    Vector3 movementDirection;

    public MonsterWalkState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = groundData.WalkSpeedModifier;
        base.Enter();
        StartAnimation(stateMachine.Monster.AnimationData.WalkParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Monster.AnimationData.WalkParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (IsInChasingRange())
        {
            walkingTime = 0f;
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }
        
        if (walkingTime == 0f)
        {
            walkingDuration = Random.Range(2, 6);
            movementDirection = GetMovementDirection();
        }

        if (walkingTime < walkingDuration)
        {
            Walk();
            walkingTime += Time.deltaTime;
        }
        else
        {
            walkingTime = 0f;
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    private void Walk()
    {
        Rotate(movementDirection);

        Move(movementDirection);
    }

    private Vector3 GetMovementDirection()
    {
        int randomX = Random.Range(-1, 2);
        int randomZ = Random.Range(-1, 2);
        Vector3 dir = new Vector3(randomX, 0, randomZ).normalized;
        
        return dir;
    }
}
