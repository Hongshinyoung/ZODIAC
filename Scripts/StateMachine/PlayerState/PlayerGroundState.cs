using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }

    protected override void MovementCanceled(InputAction.CallbackContext context)
    {
        if (stateMachine.MovementInput == Vector2.zero) return;

        stateMachine.ChangeState(stateMachine.IdleState);

        base.MovementCanceled(context);
    }

    protected override void AttackPerformed(InputAction.CallbackContext context)
    {
        if (Cursor.lockState == CursorLockMode.Locked) 
        {
            base.AttackPerformed(context);
            stateMachine.ChangeState(stateMachine.AttackState);
        }
    }

    protected override void LiftPerformed(InputAction.CallbackContext context)
    {
        base.LiftPerformed(context);
        stateMachine.ChangeState(stateMachine.LiftState);
    }

    protected override void JumpPerformed(InputAction.CallbackContext context)
    {
        base.JumpPerformed(context);
        stateMachine.ChangeState(stateMachine.JumpState);
    }

}
