using UnityEngine;

public class Teleport : ActivePlayerTrigger
{
    [SerializeField] private Transform destination;

    protected override void OnTriggerEnter(Collider other)
    {   
        if(IsCollisionWithLayer(other.gameObject, playerLayer))
        {
            other.transform.position = destination.position;
            SoundManager.Instance.PlaySound("TeleporterSFX", 0.2f);
        }
    }
}
