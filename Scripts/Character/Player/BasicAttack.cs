using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public GameObject prefab;          
    public Transform spawnPoint;
    public Transform playerTransform;  

    public void AttackShoot()
    {
        int damage = GameManager.Instance.Player.Data.attack;
        float attackSpeed = GameManager.Instance.Player.Data.attackspeed;
        float maxdistance = GameManager.Instance.Player.Data.attackmaxdistance;

        GameObject arrow = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        Arrow arrowScript = arrow.GetComponent<Arrow>();
        if (arrowScript != null)
        {
            arrowScript.Initialize(playerTransform.forward, attackSpeed, maxdistance, damage);
        }
    }
}
