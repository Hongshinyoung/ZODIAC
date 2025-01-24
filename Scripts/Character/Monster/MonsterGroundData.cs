using System;
using UnityEngine;

[Serializable]
public class MonsterGroundData
{
    [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed = 2f;
    [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping = 5f;

    [field: Header("IdleData")]

    [field: Header("WalkData")]
    [field: SerializeField][field: Range(0f, 2f)] public float WalkSpeedModifier = 1f;

    [field: Header("ChaseData")]
    [field: SerializeField][field: Range(0f, 5f)] public float ChaseSpeedModifier = 3f;
    [field: SerializeField] public float PlayerChasingRange = 20f;
}
