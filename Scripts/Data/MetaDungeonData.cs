using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterSpawnData
{
    public string SpawnMonster;
    public string MonsterCount;

    public MonsterSpawnData(string spawnMonster, string monsterCount)
    {
        SpawnMonster = spawnMonster;
        MonsterCount = monsterCount;
    }
}

[System.Serializable]
public class MetaDungeonData : DataBase<MetaDungeonData>
{
    public string StageId;
    public string ObjectName;
    [SerializeField] private string SpawnMonster;
    [SerializeField] private string MonsterCount;
    public List<MonsterSpawnData> MonsterSpawnList = new List<MonsterSpawnData>();

    public override void SetData(MetaDungeonData metaData)
    {
        this.StageId = metaData.StageId;
        this.ObjectName = metaData.ObjectName;
        AddMonsterSpawnData(metaData.SpawnMonster, metaData.MonsterCount);
    }

    private void AddMonsterSpawnData(string spawnMonster, string monsterCount)
    {
        if (string.IsNullOrEmpty(spawnMonster) == false)
        {
            MonsterSpawnData monsterSpawnData = new MonsterSpawnData(spawnMonster, monsterCount);
            MonsterSpawnList.Add(monsterSpawnData);
        }
    }
}
[System.Serializable]
public class DungeonDataList : DataBaseList<string, MetaDungeonData, MetaDungeonData>
{
    public List<MetaDungeonData> Dungeon;
    public override void SetData(List<MetaDungeonData> metaDataList)
    {
        datas = new Dictionary<string, MetaDungeonData>(metaDataList.Count); //딕셔너리 초기용량 지정
        metaDataList.ForEach(obj =>
        {
            MetaDungeonData dungeon = new MetaDungeonData();
            dungeon.SetData(obj);
            datas.Add(dungeon.StageId, dungeon);
        });
    }
}