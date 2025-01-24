using UnityEngine;


public class PlayerJumpState : PlayerBaseState
{
    private bool hasJumped = false; 

    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        hasJumped = false; 
        stateMachine.IsGrounded = false; 
        StartAnimation(stateMachine.Player.AnimationData.JumpParameterHash); 
        PerformJump(); 
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.JumpParameterHash); 
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.Player.Rigidbody.velocity.y < 0)
        {
            CheckGrounded();
        }
    }

    private void PerformJump()
    {
        if (hasJumped) return; 

        hasJumped = true;
        stateMachine.Player.Rigidbody.velocity = new Vector3(stateMachine.Player.Rigidbody.velocity.x,0,stateMachine.Player.Rigidbody.velocity.z);

        Vector3 jumpForce = Vector3.up * stateMachine.Player.Data.jumpforce;
        stateMachine.Player.Rigidbody.AddForce(jumpForce, ForceMode.Impulse);
    }

    private void CheckGrounded()
    {
        float offset = 0.5f;
        float checkDistance = 1.2f;
        Vector3 playerPosition = stateMachine.Player.transform.position + Vector3.up * offset;
        Vector3 direction = Vector3.down;

        RaycastHit hit;
        bool isGrounded = Physics.Raycast(playerPosition, direction, out hit, checkDistance);

        if (isGrounded && stateMachine.Player.Rigidbody.velocity.y <= 0.1f)
        {

            if (stateMachine.isRunning && stateMachine.MovementInput != Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.RunState);
            }
            else if (stateMachine.MovementInput != Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.WalkState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }
}