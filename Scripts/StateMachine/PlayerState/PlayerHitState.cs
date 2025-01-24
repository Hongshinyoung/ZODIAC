using UnityEngine;

public class PlayerHitState : PlayerBaseState
{
    private float invincibleTime = 1f;
    private float hitAnimationDuration = 0.5f;
    private bool isInvincible = false;
    private float timer = 0f;
    private Color originalColor;
    private Renderer playerRenderer;

    public PlayerHitState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.Player.Data.movementspeedmodifier = 1f;
        StartAnimation(stateMachine.Player.AnimationData.HitParameterHash);
        isInvincible = true;
        timer = 0f;

        playerRenderer = stateMachine.Player.GetComponentInChildren<Renderer>();
        if (playerRenderer != null)
        {
            originalColor = playerRenderer.material.color;
            ChangeColor(Color.red);
        }
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.HitParameterHash);
        isInvincible = false;

        if (playerRenderer != null)
        {
            ChangeColor(originalColor);
        }
    }

    public override void Update()
    {
        base.Update();

        timer += Time.deltaTime;

        if (timer >= invincibleTime)
        {
            isInvincible = false;
        }

        if (timer >= hitAnimationDuration && !isInvincible)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    private void ChangeColor(Color color)
    {
        if (playerRenderer != null)
        {
            playerRenderer.material.color = color;
        }
    }
}
