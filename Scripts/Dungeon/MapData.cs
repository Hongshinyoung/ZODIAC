using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    public List<Transform> monsterSpawnPoints;
    public string mapName;
    public void SetMapData(MetaDungeonData data)
    {
        this.mapName = data.ObjectName;
    }

    public Transform GetMonsterSpawnPoint(int index)
    {
        if(monsterSpawnPoints == null || monsterSpawnPoints.Count == 0) return null;

        Transform spawnPoint = monsterSpawnPoints[index];
        float randomOffset = Random.Range(-5f, 5f);
        Vector3 offset = spawnPoint.forward * randomOffset;
        spawnPoint.position += offset;
        return spawnPoint;
    }
}
