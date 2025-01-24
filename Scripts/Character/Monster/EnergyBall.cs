using System;
using System.Collections;
using UnityEngine;

public class EnergyBall : MonsterSkill
{
    [SerializeField] private GameObject targetPoint;
    [SerializeField] private float speed = 20.0f;
    [SerializeField] private float maxScale = 1.0f;
    [SerializeField] private float scaleSpeed = 0.5f;

    Vector3 defaultPosition;
    Vector3 defaultScale;
    Vector3 targetWorldPosition;
    Vector3 parentWorldPosition;
    private bool canMove = false;

    protected override void Awake()
    {
        base.Awake();
        defaultPosition = transform.localPosition;
        Debug.Log(defaultPosition);
        defaultScale = transform.localScale;
        targetPoint.SetActive(false);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        ResetSkill();
        PlayParticleSystems();

        StartCoroutine(CastingEnergy(2.0f));
        StartCoroutine(RemoveSkill(5.0f));
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            MoveEnergyBall();
        }
    }

    private void OnDisable()
    {
        canMove = false;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    public override void ResetSkill()
    {
        transform.localPosition = defaultPosition;
        transform.localScale = defaultScale;
    }

    private void TargetTing()
    {
        targetWorldPosition = target.transform.position;
        parentWorldPosition = transform.parent ? transform.parent.position : transform.position;
    }

    private void MoveEnergyBall()
    {
        if (target == null) return;

        parentWorldPosition = transform.parent ? transform.parent.position : transform.position;
        Vector3 direction = (targetWorldPosition - transform.position).normalized;

        float distance = Vector3.Distance(transform.position, targetWorldPosition);

        if (transform.localScale.x < maxScale)
        {
            transform.localScale += Vector3.one * scaleSpeed * Time.fixedDeltaTime;
        }

        if (distance > 0.1f)
        {
            transform.position += direction * speed * Time.fixedDeltaTime;
        }
        else
        {
            StartCoroutine(ShowHitEffect(2.0f));
        }
    }

    private IEnumerator CastingEnergy(float delay)
    {
        yield return new WaitForSeconds(delay);
        TargetTing();
        canMove = true;
    }
}
