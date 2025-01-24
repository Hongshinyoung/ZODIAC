using UnityEngine;

public abstract class ActivePlayerTrigger : MonoBehaviour
{
    [SerializeField] protected LayerMask playerLayer;


    protected virtual void OnTriggerStay(Collider other) { }
    protected virtual void OnTriggerEnter(Collider other) { }

    protected bool IsCollisionWithLayer(GameObject obj, LayerMask layer)
    {
        return ((1 << obj.layer) & layer) != 0;
    }
}
