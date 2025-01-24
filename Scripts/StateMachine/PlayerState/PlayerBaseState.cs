using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() 
    {
        AddInputActionCallbacks();
    }

    public virtual void Exit() 
    {
        RemoveActionCallbacks();
    }

    public virtual void Update()
    {
        MovementInput();

        if (stateMachine.isRunning && stateMachine.MovementInput != Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.RunState);
        }
    }

    public virtual void FixedUpdate() 
    {
        Move();
    }

    protected virtual void AddInputActionCallbacks()
    {
        PlayerController input = stateMachine.Player.PlayerController;
        input.playerActions.Move.canceled += MovementCanceled;
        input.playerActions.Run.performed += RunPerformed;
        input.playerActions.Run.canceled += RunCanceled;
        input.playerActions.Jump.performed += JumpPerformed;
        input.playerActions.Attack.performed += AttackPerformed;
        input.playerActions.Attack.canceled += AttackCanceled;
        input.playerActions.Interact.performed += LiftPerformed;
        input.playerActions.ItemSlot.performed += InventoryUI;
        input.playerActions.Setting.performed += SettingUI;
    }

    protected virtual void RemoveActionCallbacks()
    {
        PlayerController input = stateMachine.Player.PlayerController;
        input.playerActions.Move.canceled -= MovementCanceled;
        input.playerActions.Run.performed -= RunPerformed;
        input.playerActions.Run.canceled -= RunCanceled; 
        input.playerActions.Jump.performed -= JumpPerformed;
        input.playerActions.Attack.performed -= AttackPerformed;
        input.playerActions.Attack.canceled -= AttackCanceled;
        input.playerActions.Interact.performed -= LiftPerformed;
        input.playerActions.ItemSlot.performed -= InventoryUI;
        input.playerActions.Setting.performed -= SettingUI;
    }

    protected void StartAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, false);
    }

    protected virtual void RunPerformed(InputAction.CallbackContext context) 
    {
        stateMachine.isRunning = true;

        if (stateMachine.MovementInput != Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.RunState);
        }
    }

    protected virtual void RunCanceled(InputAction.CallbackContext context) 
    {
        stateMachine.isRunning = false;

        if (stateMachine.MovementInput != Vector2.zero)
        {
            stateMachine.Player.Data.movementspeedmodifier = 1f;
            stateMachine.ChangeState(stateMachine.WalkState);
        }
    }

    protected virtual void MovementCanceled(InputAction.CallbackContext context) { }

    protected virtual void AttackPerformed(InputAction.CallbackContext context)  { }

    protected virtual void AttackCanceled(InputAction.CallbackContext context) { }

    private void InventoryUI(InputAction.CallbackContext context) 
    {
        if (UIManager.Instance.Get<UIInventory>() == null)
        {
            var inventory = UIManager.Instance.Show<UIInventory>();
            inventory.ToggleUI();
            return;
        }
        UIManager.Instance.Get<UIInventory>().ToggleUI();
    }

    private void SettingUI(InputAction.CallbackContext context) 
    {
        UIPopupSetting settingPopup = UIManager.Instance.Get<UIPopupSetting>();
        if (settingPopup != null)
        {
            UIManager.Instance.Hide<UIPopupSetting>();
        }
        else
        {
            UIManager.Instance.Show<UIPopupSetting>();
        }
    }

    protected virtual void LiftPerformed(InputAction.CallbackContext context) { } 

    protected virtual void JumpPerformed(InputAction.CallbackContext context) { }

    private void MovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.PlayerController.playerActions.Move.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();
        Movement(movementDirection);
        Rotate(movementDirection);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();
        
        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }

    private void Movement(Vector3 direction)
    {
        float movementSpeed = MovementSpeed();
        Vector3 targetPosition = stateMachine.Player.Rigidbody.position + ((direction * movementSpeed ) * Time.fixedDeltaTime);

        stateMachine.Player.Rigidbody.MovePosition(targetPosition);
    }

    private float MovementSpeed() 
    {
        float moveSpeed = stateMachine.Player.Data.walkspeed * stateMachine.Player.Data.movementspeedmodifier;
        return moveSpeed;
    }

    private void Rotate(Vector3 direction) 
    {
        if (direction != Vector3.zero) 
        {
            Transform playerTransform = stateMachine.Player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.Player.Data.rotationdamping * Time.deltaTime);
        }
    }
}
