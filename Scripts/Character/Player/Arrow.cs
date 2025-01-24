using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Vector3 direction;
    private float speed;             
    private Vector3 startPosition;
    private float maxDistance;       
    private int basicDamage;

    public void Initialize(Vector3 direction, float speed, float maxDistance, int damage)
    {
        this.direction = direction;
        this.speed = speed;
        this.maxDistance = maxDistance;
        this.basicDamage = damage;

        startPosition = transform.position;
    }

    void Update()
    {
        Shoot();
    }

    public void Shoot() 
    {
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(startPosition, transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster")) 
        {
            var target = other.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeBasicAttackDamage(basicDamage);
            }
        }
        Destroy(gameObject);
    }
}
