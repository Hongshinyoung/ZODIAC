public class MonsterGroundState : MonsterBaseState
{
    public MonsterGroundState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Monster.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Monster.AnimationData.GroundParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }
}
