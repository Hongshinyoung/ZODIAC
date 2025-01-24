using UnityEngine;

public class MonsterAttackParts : MonoBehaviour
{
    private int damage;
    private BoxCollider hitBox;

    private void Awake()
    {
        hitBox = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        hitBox.enabled = true;
    }

    public void SetPartsDamage(int damage)
    {
        this.damage = damage;
    }

    private void OnCollisionEnter(Collision other)
    {
        var target = other.gameObject.GetComponent<IDamageable>() as Player;
        if ((target != null) && other.gameObject.CompareTag("Player"))
        {
            target.TakeBasicAttackDamage(damage);
            hitBox.enabled = false;
        }
    }
}
