using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string runParameterName = "Run";

    [SerializeField] private string jumpParameterName = "@Jump";

    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string hitParameterName = "@Hit";

    [SerializeField] private string liftParameterName = "@Lift";
    [SerializeField] private string liftWalkParameterName = "LiftWalk";

    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int HitParameterHash { get; private set; }
    public int LiftParameterHash { get; private set; }
    public int LiftWalkParameterHash { get; private set; }


    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);
        JumpParameterHash = Animator.StringToHash(jumpParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        HitParameterHash = Animator.StringToHash(hitParameterName);
        LiftParameterHash = Animator.StringToHash(liftParameterName);
        LiftWalkParameterHash = Animator.StringToHash(liftWalkParameterName);
    }
}
