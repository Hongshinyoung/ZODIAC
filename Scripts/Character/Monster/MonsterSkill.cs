using System.Collections;
using System.Linq;
using UnityEngine;

public class MonsterSkill : MonoBehaviour
{
    protected Player target;
    protected int damage;
    protected ParticleSystem[] particleSystems;
    [SerializeField] private ParticleSystem hitParticle;

    protected virtual void Awake()
    {
        target = GameManager.Instance.Player;
        particleSystems = GetComponentsInChildren<ParticleSystem>().
            Where(p => p.gameObject.name != "HitEffect").ToArray();
        hitParticle.gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        var target = other.gameObject.GetComponent<IDamageable>() as Player;
        if ((target != null) && other.gameObject.CompareTag("Player"))
        {
            target.TakeBasicAttackDamage(damage);
            hitParticle.gameObject.SetActive(true);

            StartCoroutine(ShowHitEffect(0.2f));
        }
    }

    protected IEnumerator ShowHitEffect(float second)
    {
        hitParticle.Stop();
        hitParticle.Play();

        yield return new WaitForSeconds(second);
        hitParticle.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    protected IEnumerator RemoveSkill(float second)
    {
        yield return new WaitForSeconds(second);
        gameObject.SetActive(false);
    }

    public void SetSkillDamage(int damage)
    {
        this.damage = damage * 5;
    }

    public virtual void ResetSkill()
    {

    }

    protected void PlayParticleSystems()
    {
        foreach (var particle in particleSystems)
        {
            if ((particle != null))
            {
                particle.Stop();
                particle.Play();
            }
        }
    }
}
