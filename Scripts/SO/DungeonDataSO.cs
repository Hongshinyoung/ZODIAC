using UnityEngine;

[CreateAssetMenu(fileName = "MapDataSO", menuName = "ScriptableObjects/MapDataSO", order = 1)]
public class DungeonDataSO : ScriptableObject
{
    public int Stage;
    public string ObjectType;
    public string ObjectName;
    public int[] Position;
    public int[] Rotation;
    public float[] Scale;
    public int Count;
}