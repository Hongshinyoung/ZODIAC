public class TownScene : SceneBase
{
    public void OnEnterDungeon()
    {
        SceneLoadManager.Instance.ChangeScene("InGameDungeonScene", () =>
        {}, UnityEngine.SceneManagement.LoadSceneMode.Single);
        UIManager.Instance.Get<UIEquipment>().ToggleUI();
    }
}