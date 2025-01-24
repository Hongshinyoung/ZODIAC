using System.Collections;
using UnityEngine;

public class BellyAttack : MonsterSkill
{
    private BoxCollider hitBox;

    protected override void Awake()
    {
        base.Awake();
        hitBox = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        PlayParticleSystems();
        StartCoroutine(RemoveSkill(3.0f));
        StartCoroutine(ActiveHitBox());
    }

    private void OnDisable()
    {
        hitBox.enabled = false;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    private IEnumerator ActiveHitBox()
    {
        yield return new WaitForSeconds(1.5f);
        hitBox.enabled = true;
    }
}
