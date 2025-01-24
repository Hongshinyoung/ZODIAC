using System.Collections.Generic;
using UnityEngine;
public class DungeonManager : MonoBehaviour
{
    private Dictionary<GameObject, Vector3> initialMonsterPositions = new Dictionary<GameObject, Vector3>();
    public Dictionary<GameObject, Vector3> InitialMonsterPositions => initialMonsterPositions;
    public PuzzleManager PuzzleManager { get; private set; }
    private string curStageId;
    private int starIdx;
    private int stageIdx;
    

    // 매니저 파괴
    private void OnDestroy()
    {
        if (PuzzleManager != null) Destroy(PuzzleManager.gameObject);

        PuzzleManager = null;
        initialMonsterPositions.Clear();
        GameManager.Instance.ExitDungeon();
        Destroy(UIManager.Instance.Show<PlayerUI>());
        if (UIManager.Instance.Show<PlayerUI>() != null)
        {
            UIManager.Instance.Hide<PlayerUI>();
        }
    }


    public void InitializeDungeon(string stageId, int starIndex, int stageIndex)
    {
        starIdx = starIndex;
        stageIdx = stageIndex;
        PuzzleManager = new GameObject("PuzzleManager").AddComponent<PuzzleManager>();
        UIManager.Instance.Show<PlayerUI>();
        curStageId = stageId;
        MetaDungeonData stageData = DataManager.Instance.DungeonDB.LoadData(stageId);
        SpawnStageObjects(curStageId, stageData);
    }


    private void SpawnStageObjects(string stageId, MetaDungeonData stageData)
    {
        if (!DataManager.Instance.DungeonDB.datas.ContainsKey(stageId))
        {
            return;
        }
        GameObject mapPrefab = ResourceManager.Instance.LoadAsset<GameObject>(stageId, eAssetType.Prefab, eCategoryType.Map);

        if (mapPrefab == null)
        {
            return;
        }

        GameObject instance = Instantiate(mapPrefab, transform);
        MapData mapData = instance.GetComponent<MapData>();
        if (mapData == null)
        {
            return;
        }

        mapData.SetMapData(stageData);
        foreach(var monster in stageData.MonsterSpawnList)
        {
            SpawnMonster(monster.SpawnMonster, monster.MonsterCount, mapData);
        }
    }


    private void SpawnMonster(string SpawnMonster, string MonsterCount, MapData mapData)
    {
        if (string.IsNullOrEmpty(SpawnMonster) || string.IsNullOrEmpty(MonsterCount))
        {
            return;
        }

        string[] spawnMonsterArray = SpawnMonster.Split('/');
        string[] monsterCountArray = MonsterCount.Split('/');
        if (spawnMonsterArray.Length != monsterCountArray.Length)
        {
            return;
        }

        for (int i = 0; i < spawnMonsterArray.Length; i++) //몬스터 종류 만큼 돌리기
        {
            string monsterId = spawnMonsterArray[i].Trim(); //공백 제거
            if (int.TryParse(monsterCountArray[i], out int count)) //스폰 할 갯수 정수형으로 변환
            {
                if(count <= 0)
                {
                    continue;
                }

                GameObject monsterPrefab = ResourceManager.Instance.LoadAsset<GameObject>(monsterId, eAssetType.Prefab, eCategoryType.Monster);

                if (monsterPrefab == null)
                {
                    continue;
                }
                for (int j = 0; j < count; j++) // 몬스터 수
                {
                    Transform spawnPoint = mapData.GetMonsterSpawnPoint(i);
                    if (spawnPoint == null)
                    {
                        return;
                    }
                    GameObject monsterInstance = Instantiate(monsterPrefab, spawnPoint);
                    initialMonsterPositions.Add(monsterInstance, spawnPoint.position);
                }
            }                  
        }
    }


    public void StageClear()
    {
        Cursor.lockState = CursorLockMode.None;
        SoundManager.Instance.PlaySound("ClearSFX", 0.2f);
        UIManager.Instance.Show<UIPopupStageClear>();

        ClearData clearData = GameManager.Instance.clearData;
        if (clearData == null) { return; }
        clearData.stageCleared[starIdx, stageIdx] = true;

        
        if(clearData.stageCleared.GetLength(1) > stageIdx) // 현재 스테이지가 6스테이지 보다 작으면
        {
            clearData.stageCleared[starIdx, stageIdx + 1] = true; // 다음 스테이지 언락
            GameManager.Instance.clearData.stageCleared[starIdx, stageIdx + 1] = true;
        }
        else if(clearData.stageCleared.GetLength(1) == stageIdx) // 현재 스테이지가 6이면, 
        {
            clearData.stageCleared[starIdx + 1, 0] = true; //다음 별 1스테이지 언락
            GameManager.Instance.clearData.stageCleared[starIdx + 1, 0] = true;
        }
    }
}
