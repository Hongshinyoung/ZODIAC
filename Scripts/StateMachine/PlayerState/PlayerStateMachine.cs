using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; set; }

    public Vector2 MovementInput { get; set; }

    public Transform MainCameraTransform { get; set; }

    public PlayerIdleState IdleState { get; private set; }

    public PlayerWalkState WalkState { get; private set; }

    public PlayerRunState RunState { get; private set; }

    public PlayerJumpState JumpState { get; private set; }

    public PlayerAttackState AttackState { get; private set; }

    public PlayerHitState HitState { get; private set; }

    public PlayerLiftState LiftState { get; private set; }

    public bool IsGrounded = false;

    public bool isRunning = false;

    public PlayerStateMachine(Player player) 
    {
        this.Player = player;

        MainCameraTransform = Camera.main.transform;

        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        RunState = new PlayerRunState(this);
        JumpState = new PlayerJumpState(this);
        AttackState = new PlayerAttackState(this);
        HitState = new PlayerHitState(this);
        LiftState = new PlayerLiftState(this);
    }
}
