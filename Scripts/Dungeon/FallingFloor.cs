using UnityEngine;

public class FallingFloor : ActivePlayerTrigger
{
    private float fallDelay = 2f;
    private float fallingSpeed = 10f;
    private float rePositionDelay = 5f;
    private bool isFalling = false;
    private Vector3 initPos;

    private void OnCollisionEnter(Collision collision)
    {
        if (IsCollisionWithLayer(collision.gameObject, playerLayer))
        {
            Invoke("Fall", fallDelay);
            Invoke("RePosition", rePositionDelay);
        }
    }

    private void Start()
    {
        initPos = transform.position;
    }

    private void Update()
    {
        if (isFalling)
        {
            Fall();
        }
    }

    private void Fall()
    {
        isFalling = true;
        transform.position -= new Vector3(0, fallingSpeed * Time.deltaTime, 0);
    }

    private void RePosition()
    {
        isFalling = false;
        transform.position = initPos;
    }


}
