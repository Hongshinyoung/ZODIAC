public abstract class StateMachine
{
    public IState currentState {get; private set;}

    public void ChangeState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void FixedUpdate() 
    {
        currentState?.FixedUpdate();
    }
}
