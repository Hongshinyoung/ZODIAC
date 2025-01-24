using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class UIPopupSetting : UIBase
{
    private Player player;

    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider mouseSlider;

    private CinemachineVirtualCamera virtualCamera;
    private CinemachinePOV povComponent;

    [SerializeField] private GameObject GameExitUI;
    [SerializeField] private GameObject StageExitUI;


    private void Start()
    {
        player = GameManager.Instance.Player;

        virtualCamera = player.PlayerLook.VirtualCamera;
        povComponent = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
     
        if (mouseSlider != null && povComponent != null)
        {
            mouseSlider.maxValue = 300f; 
            mouseSlider.minValue = 10f;
            mouseSlider.value = povComponent.m_HorizontalAxis.m_MaxSpeed;

            mouseSlider.onValueChanged.AddListener((value) => MouseSensitivity(value));
        }
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void TogleOnSound()
    {
        soundSlider.value = 0.4f;
        SoundManager.Instance.SetVolume(soundSlider.value);
    }

    public void TogleOffSound()
    {
        SoundManager.Instance.SetVolume(0f);

        if (soundSlider != null)
        {
            soundSlider.value = 0f;
        }
    }

    public void SoundVolume()
    {
        SoundManager.Instance.SetVolume(soundSlider.value);
    }

    public void MouseSensitivity(float sensitivity)
    {
        if (player != null && player.PlayerLook != null)
        {
            player.PlayerLook.UpdateSensitivity(sensitivity);
            PlayerPrefs.SetFloat("MouseSensitivity", sensitivity); 
            PlayerPrefs.Save();
        }
    }

    public void SaveGame()
    {
        SoundManager.Instance.PlaySound("ChoiceSFX", 0.2f);
        GameManager.Instance.SaveGame();
    }

    public void GameQuit()
    {
        SoundManager.Instance.PlaySound("ChoiceSFX", 0.2f);
        Application.Quit();
    }

    public void StageQuit()
    {
        SoundManager.Instance.PlaySound("ChoiceSFX", 0.2f);
        SceneLoadManager.Instance.ChangeScene("InGameTownScene", () => { }, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void Exit()
    {
        SoundManager.Instance.PlaySound("ChoiceSFX", 0.2f);
        string currentSceneName = SceneManager.GetActiveScene().name;

        GameExitUI.SetActive(false);
        StageExitUI.SetActive(false);

        if (currentSceneName == "InGameDungeonScene")
        {
            StageExitUI.SetActive(true);
        }
        else if (currentSceneName == "InGameTownScene")
        {
            GameExitUI.SetActive(true);
        }

    }
}
