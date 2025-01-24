using UnityEngine;

public class MonsterStateMachine : StateMachine
{
    public Monster Monster { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; }

    public GameObject Target { get; private set; }
    public MonsterGroundState GroundState { get; }
    public MonsterIdleState IdleState { get; }
    public MonsterWalkState WalkState { get; }
    public MonsterChasingState ChasingState { get; }
    public MonsterBattleState BattleState { get; }
    public MonsterBaseAttackState BaseAttackState { get; }
    public MonsterSkillState SkillState { get; }
    public MonsterHitState HitState { get; }
    public MonsterDieState DieState { get; }

    public MonsterStateMachine(Monster monster)
    {
        this.Monster = monster;
        Target = GameObject.FindGameObjectWithTag("Player");

        GroundState = new MonsterGroundState(this);
        IdleState = new MonsterIdleState(this);
        ChasingState = new MonsterChasingState(this);
        WalkState = new MonsterWalkState(this);
        BattleState = new MonsterBattleState(this);
        BaseAttackState = new MonsterBaseAttackState(this);
        SkillState = new MonsterSkillState(this);
        HitState = new MonsterHitState(this);
        DieState = new MonsterDieState(this);

        MovementSpeed = Monster.Data.GroundData.BaseSpeed;
        RotationDamping = Monster.Data.GroundData.BaseRotationDamping;
    }
}
