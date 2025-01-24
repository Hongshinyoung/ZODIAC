using UnityEngine;

public class UIPopupGameOver : UIBase
{
    private void OnEnable()
    {
        Time.timeScale = 0.0f;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }

    public void GoToLobby()
    {
        Hide();
        SoundManager.Instance.PlaySound("ChoiceSFX", 0.2f);
        SceneLoadManager.Instance.ChangeScene("InGameTownScene", () =>
        { }, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

}
