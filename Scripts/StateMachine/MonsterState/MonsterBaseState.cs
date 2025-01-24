using UnityEngine;

public class MonsterBaseState : IState
{
    protected MonsterStateMachine stateMachine;
    protected readonly MonsterGroundData groundData;

    public MonsterBaseState(MonsterStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        groundData = stateMachine.Monster.Data.GroundData;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Update()
    {
        stateMachine.Monster.skillCoolTime += Time.deltaTime;
    }

    public virtual void FixedUpdate() { }


    protected void StartAnimation(int animationHash)
    {
        stateMachine.Monster.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Monster.Animator.SetBool(animationHash, false);
    }

    protected void Move(Vector3 movementDirection)
    {
        float movementSpeed = GetMovementSpeed();
        movementDirection.y = 0;
        movementDirection = movementDirection.normalized;
        stateMachine.Monster.Controller.Move(((movementDirection * movementSpeed) + stateMachine.Monster.ForceReceiver.Movement) * Time.deltaTime);
    }

    protected float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }

    protected void LookPlayer()
    {
        Vector3 movementDirection = GetPlayerDirection();

        Rotate(movementDirection);
    }

    protected Vector3 GetPlayerDirection(bool isNormalized = true)
    {
        Vector3 dir = (stateMachine.Target.transform.position - stateMachine.Monster.transform.position);
        if(isNormalized)
            dir = dir.normalized;
        return dir;
    }

    protected void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            var rotation = Quaternion.Lerp(stateMachine.Monster.transform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
            rotation.x = 0;
            rotation.z = 0;
            stateMachine.Monster.transform.rotation = rotation;
        }
    }

    protected bool IsInChasingRange()
    {
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Monster.transform.position).sqrMagnitude;
        return playerDistanceSqr <= groundData.PlayerChasingRange * groundData.PlayerChasingRange;
    }

    protected bool IsInAttackRange()
    {
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Monster.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Monster.Data.AttackRange * stateMachine.Monster.Data.AttackRange;
    }

    protected bool IsInSkillRange()
    {
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Monster.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Monster.Data.SkillRange * stateMachine.Monster.Data.SkillRange;
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}
