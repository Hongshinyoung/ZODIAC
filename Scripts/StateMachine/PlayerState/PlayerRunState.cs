public class PlayerRunState : PlayerGroundState
{
    public PlayerRunState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Player.Data.movementspeedmodifier = stateMachine.Player.Data.runspeedmodifier;
        StartAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.isRunning = false;
        StopAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }

}
