using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Player.Data.movementspeedmodifier = 0f;

        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f; 

        if (cameraForward.sqrMagnitude > 0f) 
        {
            stateMachine.Player.transform.forward = cameraForward.normalized;
        }
        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    protected override void AttackCanceled(InputAction.CallbackContext context)
    {
        base.AttackCanceled(context);
        stateMachine.ChangeState(stateMachine.IdleState);
    }
}
