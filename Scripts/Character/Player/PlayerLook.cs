using Cinemachine;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private Player player;

    public CinemachineVirtualCamera VirtualCamera;
    private CinemachinePOV povComponent;

    private float currentSensitivity;

    private void Start()
    {
        povComponent = VirtualCamera.GetCinemachineComponent<CinemachinePOV>();

        currentSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 100f); 
        ApplySensitivity(currentSensitivity);
    }

    private void LateUpdate()
    {
        HandleCameraMovement();
    }
    
    private void HandleCameraMovement()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            povComponent.m_HorizontalAxis.m_InputAxisName = "";
            povComponent.m_VerticalAxis.m_InputAxisName = "";

            povComponent.m_HorizontalAxis.m_MaxSpeed = 0;
            povComponent.m_VerticalAxis.m_MaxSpeed = 0;
        }
        else
        {
            povComponent.m_HorizontalAxis.m_InputAxisName = "Mouse X";
            povComponent.m_VerticalAxis.m_InputAxisName = "Mouse Y";

            povComponent.m_HorizontalAxis.m_MaxSpeed = currentSensitivity;
            povComponent.m_VerticalAxis.m_MaxSpeed = currentSensitivity;
        }
    }

    public void UpdateSensitivity(float newSensitivity)
    {
        currentSensitivity = newSensitivity; // 새로운 민감도 저장
        ApplySensitivity(newSensitivity);
    }

    private void ApplySensitivity(float sensitivity)
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            povComponent.m_HorizontalAxis.m_MaxSpeed = sensitivity;
            povComponent.m_VerticalAxis.m_MaxSpeed = sensitivity;
        }
    }
}


