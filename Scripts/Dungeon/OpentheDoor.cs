using UnityEngine;

public class OpentheDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    private void OnDisable()
    {
        door.SetActive(true);
    }
}
