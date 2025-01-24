using UnityEngine;

public class JumpingFloor : ActivePlayerTrigger
{
    private float jumpForce = 25f;
    protected override void OnTriggerEnter(Collider other)
    {
        if(IsCollisionWithLayer(other.gameObject, playerLayer))
        {
            Rigidbody rb =  other.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

}
