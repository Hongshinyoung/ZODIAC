using Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public PlayerData Data { get; private set; }

    public Animator Animator { get; private set; }

    public PlayerController PlayerController { get; private set; }

    public Rigidbody Rigidbody { get; private set; }

    public PlayerLook PlayerLook { get; private set; }

    public Transform holdPosition;

    private PlayerStateMachine stateMachine;

    private BasicAttack basicAttack;



    private void Awake()
    {
        GameManager.Instance.Player = this;

        Data = DataManager.Instance.PlayerDB.LoadData("PLAYER001");

        AnimationData.Initialize();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();
        PlayerController = GetComponent<PlayerController>();
        basicAttack = GetComponent<BasicAttack>();

        stateMachine = new PlayerStateMachine(this);
        stateMachine.ChangeState(stateMachine.IdleState);

        var virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        PlayerLook = virtualCamera.GetComponent<PlayerLook>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public void StatUpdate(ItemData data, bool equipment)
    {
        switch (data.itemType)
        {
            case ItemType.Weapon:
                if (equipment)
                    Data.attack += data.stat;
                else
                    Data.attack -= data.stat;
                Debug.Log(Data.attack);
                break;

            case ItemType.Shield:
                if (equipment)
                    Data.hp += data.stat;
                else
                    Data.hp -= data.stat;
                Debug.Log(Data.hp);
                break;
            default:
                break;
        }
    }

    public void GainExperience(int expAmount)
    {
        if (GameManager.Instance.UserData == null)
        {
            Debug.LogError("UserData가 null입니다. 경험치를 추가할 수 없습니다.");
            return;
        }
        bool leveledUp = GameManager.Instance.UserData.AddExperience(expAmount);

        if (leveledUp)
        {
            OnLevelUp();
        }
    }

    private void OnLevelUp()
    {
        Debug.Log($"플레이어가 레벨업했습니다! 현재 레벨: {GameManager.Instance.UserData.level}");
    }

    public void TakeBasicAttackDamage(int damage)
    {
        Data.hp -= damage;
        if (Data.hp > 0)
        {
            stateMachine.ChangeState(stateMachine.HitState);
        }
        else
        {
            UIManager.Instance.Show<UIPopupGameOver>();
            Data.hp = 200;
            var collider = GetComponent<CapsuleCollider>();
            collider.enabled = false;
        }
    }
}
