using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLiftState : PlayerBaseState
{
    private GameObject pickedObject = null;
    private float pickUpRange = 10f;
    private bool hasJumped = false;

    private GameObject weaponR;

    public PlayerLiftState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        weaponR = GameObject.FindWithTag("weapon");
    }

    public override void Enter()
    {
        base.Enter();

        if (pickedObject == null && !PickUpObject())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
        else
        {weaponR.SetActive(false);
            StartAnimation(stateMachine.Player.AnimationData.LiftParameterHash);
        }
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.MovementInput != Vector2.zero)
        {
            StartAnimation(stateMachine.Player.AnimationData.LiftWalkParameterHash);
        }
        else
        {
            StopAnimation(stateMachine.Player.AnimationData.LiftWalkParameterHash);
        }

        if (stateMachine.Player.Rigidbody.velocity.y < 0)
        {
            CheckGrounded();
        }
    }

    protected override void LiftPerformed(InputAction.CallbackContext context)
    {
        DropObject();
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    protected override void JumpPerformed(InputAction.CallbackContext context)
    {
        if (hasJumped) return; 

        hasJumped = true;
        stateMachine.Player.Rigidbody.velocity = new Vector3
        (
            stateMachine.Player.Rigidbody.velocity.x,
            0,
            stateMachine.Player.Rigidbody.velocity.z
        );

        Vector3 jumpForce = Vector3.up * stateMachine.Player.Data.jumpforce;
        stateMachine.Player.Rigidbody.AddForce(jumpForce, ForceMode.Impulse);
    }

    protected override void RunPerformed(InputAction.CallbackContext context) {}

    protected override void RunCanceled(InputAction.CallbackContext context) {}

    private void CheckGrounded()
    {
        float offset = 1.0f;
        float checkDistance = 1.5f;
        Vector3 playerPosition = stateMachine.Player.transform.position + Vector3.up * offset;
        Vector3 direction = Vector3.down;

        RaycastHit hit;
        bool isGrounded = Physics.Raycast(playerPosition, direction, out hit, checkDistance);

        if (isGrounded && stateMachine.Player.Rigidbody.velocity.y <= 0.1f)
        {
            hasJumped = false; 
            stateMachine.IsGrounded = true;
        }
        else
        {
            stateMachine.IsGrounded = false;
        }
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.LiftParameterHash);
        StopAnimation(stateMachine.Player.AnimationData.LiftWalkParameterHash);

        DropObject();
        weaponR.SetActive(true);
    }


    private bool PickUpObject()
    {
        Transform playerTransform = stateMachine.Player.transform;

        Vector3 rayOrigin = playerTransform.position + Vector3.up * 1f;
        Vector3 rayDirection = playerTransform.forward;

        Debug.DrawRay(rayOrigin, rayDirection * pickUpRange, Color.red, 2.0f);
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, pickUpRange) &&
            hit.collider.CompareTag("Pickable"))
        {
            pickedObject = hit.collider.gameObject;

            SetObjectPhysics(pickedObject, true);

            pickedObject.transform.position = stateMachine.Player.holdPosition.position;
            pickedObject.transform.SetParent(stateMachine.Player.holdPosition);

            return true;
        }
        return false;
    }

    private void DropObject()
    {
        if (pickedObject != null)
        {
            SetObjectPhysics(pickedObject, false);

            pickedObject.transform.SetParent(null);
            pickedObject = null;
        }
    }

    private void SetObjectPhysics(GameObject obj, bool isKinematic)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = isKinematic;
        BoxCollider col = obj.GetComponent<BoxCollider>();
        if (col != null && isKinematic)
        {
            col.size = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if(col != null && !isKinematic)
        {
            col.size = new Vector3(1,1,1);
        }
    }

}
