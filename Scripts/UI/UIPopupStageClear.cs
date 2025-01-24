using TMPro;
using UnityEngine;

public class UIPopupStageClear : UIBase
{
    [SerializeField] private TextMeshPro rewardText;
    public override void Opened(params object[] param)
    {
        base.Opened(param);
    }

    private void Awake()
    {
        
    }

    public void GoToLobby()
    {
        Hide();
        SoundManager.Instance.PlaySound("ChoiceSFX", 0.2f);
        GameManager.Instance.SaveGame();
        GameManager.Instance.Player.Data.hp = GameManager.Instance.Player.Data.maxhp;
        SceneLoadManager.Instance.ChangeScene("InGameTownScene", () =>
        {}, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

}
