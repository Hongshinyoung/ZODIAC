using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

// C#에서 숫자 앞에 0은 지원하지 않는다. 키 값을 만들때 0을 생성하도록 하자
public enum MonsterId
{
    None,
    BigBlink,
    OneEye,
    Cyclops,
    Wreck,
    Wheel,
    Spike,
}

public enum MonsterType
{
    Nono,
    Normal,
    Boss
}

public class Monster : MonoBehaviour, IDamageable
{
    [field: Header("Reference")]
    [field: SerializeField] public MonsterStat Data { get; set; }
    public int Hp{ get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public MonsterAnimationData AnimationData { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    private MonsterStateMachine stateMachine;
    public MonsterForceReceiver ForceReceiver { get; private set; }

    private MonsterAttackParts[] attackParts;
    private bool attackPartsToggleState = false;

    public MonsterSkill skill {get; private set;}
    public float skillCoolTime;

    public int expValue = 50;

    private void Awake()
    {
        AnimationData.Initialize();
        SetData();
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<MonsterForceReceiver>();
        attackParts = GetComponentsInChildren<MonsterAttackParts>();
        foreach(var part in attackParts)
        {
            part.SetPartsDamage(Data.Stat.AttackPower);
            part.gameObject.SetActive(attackPartsToggleState);
        }

        if(Data.Type == MonsterType.Boss)
        {
            skill = GetComponentInChildren<MonsterSkill>(true);
            skill.SetSkillDamage(Data.Stat.AttackPower);
            skillCoolTime = Data.SkillTransitionTime;
        }

        stateMachine = new MonsterStateMachine(this);

        Debug.Log(Data.Id);
        Debug.Log(Data.Name);
        Debug.Log(Data.Type);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void TakeBasicAttackDamage(int basicDamage)
    {
        Player player = GameManager.Instance.Player;

        if (player == null)
        {
            Debug.LogError("GameManager.Instance.Player가 null입니다.");
            return;
        }

        stateMachine.ChangeState(stateMachine.HitState);
        Hp -= basicDamage;
        Debug.Log($"{Hp}");

        if (Hp <= 0)
        {
            player.GainExperience(expValue);
            DropItem();
            Controller.enabled = false; 
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        stateMachine.ChangeState(stateMachine.DieState);
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }

    public void ToggleNormalAttackParts()
    {
        attackPartsToggleState = !attackPartsToggleState;
        foreach (var part in attackParts)
        {
            part.gameObject.SetActive(attackPartsToggleState);
        }
    }

    private void SetData()
    {
        string objectName = Regex.Replace(gameObject.name, @"\s\(\d+\)$", "");

        objectName = objectName.Replace("(Clone)", "");

        if (Enum.TryParse(objectName, out MonsterId monsterId))
        {
            int monsterNumber = Convert.ToInt32(monsterId);

            string key = $"MONSTER{monsterNumber:D3}";

            MonsterData monsterData = DataManager.Instance.MonsterDB.LoadData(key);

            if (monsterData != null)
            {
                Data = new MonsterStat(monsterData);
                this.Hp = (int)Data.Stat.Hp;
            }
            else
            {
                Debug.Log("원하는 monsterData를 찾지 못했음");
            }
        }
    }

    private void DropItem()
    {
        GameObject itemPrefab = ResourceManager.Instance.LoadAsset<GameObject>("Item/DroppedItem", eAssetType.Prefab);
        Instantiate(itemPrefab, transform.position + Vector3.up, Quaternion.identity);
    }
}
