using UnityEngine;

public class Meteor : MonoBehaviour
{
    private Vector3 targetPosition;
    private float fallSpeed;
    private int damage;

    public void Initialize(Vector3 target, float speed, int damageAmount)
    {
        targetPosition = target;
        fallSpeed = speed;
        damage = damageAmount;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, fallSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            OnHitTarget();
        }
    }

    private void OnHitTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(targetPosition, 2f);
        foreach (var collider in hitColliders)
        {
            var target = collider.GetComponent<IDamageable>();
            if (target != null && collider.gameObject.layer == LayerMask.NameToLayer("Monster"))
            {
                target.TakeBasicAttackDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}