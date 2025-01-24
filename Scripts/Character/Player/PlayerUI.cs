using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : UIBase
{
    [Header("UI 슬라이더")]
    [SerializeField] private Slider expSlider;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider mpSlider;

    [SerializeField] private Text maxHpText;
    [SerializeField] private Text maxmpText;

    [SerializeField] private Text hpText;
    [SerializeField] private Text mpText;
    [SerializeField] private Text levelText;

    [Header("UI 스킬")]
    [SerializeField] private GameObject[] hideSkillButtons;
    [SerializeField] private Text[] skillCooldownTexts;
    [SerializeField] private Image[] skillCooldownImages;

    [SerializeField] private MeteorSkill meteorSkill;  // 메테오 스킬
    [SerializeField] private RainSkill rainSkill;      // 레인 스킬
    [SerializeField] private HealingSkill healingSkill; // 힐링 스킬

    public Player Player;

    private int maxHp = 200;
    private int maxmp = 200;


    private void Start()
    {
        InitializeSliders();

        for (int i = 0; i < hideSkillButtons.Length; i++)
        {
            hideSkillButtons[i].SetActive(false); // 초기 비활성화
        }
    }

    private void Update()
    {
        UpdateSliders();
        UpdateSkillCooldownUI();
    }

    public void ResetStat() 
    {
        Player.Data.maxhp = maxHp;
        Player.Data.mp = maxmp;
    }

    private void InitializeSliders()
    {
        Player = GameManager.Instance.Player;
        if (Player == null) return;
        
        meteorSkill = Player.GetComponent<MeteorSkill>();
        healingSkill = Player.GetComponent<HealingSkill>();
        rainSkill = Player.GetComponent<RainSkill>();

        ResetStat();

        expSlider.maxValue = GameManager.Instance.UserData.nextlevelexp;
        expSlider.value = GameManager.Instance.UserData.exp;

        hpSlider.maxValue = Player.Data.maxhp; 
        hpSlider.value = Player.Data.hp;
        maxHpText.text = Player.Data.maxhp.ToString();
        maxmpText.text = Player.Data.mp.ToString();

        mpSlider.maxValue = Player.Data.mp; 
        mpSlider.value = Player.Data.mp;
        levelText.text = GameManager.Instance.UserData.level.ToString();
    }

    private void UpdateSliders()
    {
        if (Player != null)
        {
            expSlider.value = GameManager.Instance.UserData.exp;

            hpSlider.value = Player.Data.hp;
            mpSlider.value = Player.Data.mp;
            hpText.text = Player.Data.hp.ToString();
            mpText.text = Player.Data.mp.ToString();
            levelText.text = GameManager.Instance.UserData.level.ToString();
        }
    }

    private void UpdateSkillCooldownUI()
    {
        UpdateCooldownUI(0, meteorSkill.lastActivatedTime, meteorSkill.skillDelay);
        UpdateCooldownUI(1, rainSkill.lastActivatedTime, rainSkill.skillDelay);
        UpdateCooldownUI(2, healingSkill.lastActivatedTime, healingSkill.skillDelay);
    }

    private void UpdateCooldownUI(int skillIndex, float lastActivatedTime, float skillDelay)
    {
        float remainingTime = Mathf.Max(0, (lastActivatedTime + skillDelay) - Time.time);

        if (remainingTime > 0)
        {
            hideSkillButtons[skillIndex].SetActive(true);
            skillCooldownTexts[skillIndex].text = Mathf.CeilToInt(remainingTime).ToString();
            skillCooldownImages[skillIndex].fillAmount = remainingTime / skillDelay;
        }
        else
        {
            hideSkillButtons[skillIndex].SetActive(false);
            skillCooldownTexts[skillIndex].text = "";
            skillCooldownImages[skillIndex].fillAmount = 0;
        }
    }
}
