using UnityEngine;


public class DataManager : Singleton<DataManager>
{
    private ItemDataList itemDataList;

    public ItemDataList ItemDB => itemDataList ??= LoadData<ItemDataList, MetaItemDataList>("ItemData", data =>
    {
        itemDataList = new ItemDataList();
        itemDataList.SetData(data.ItemData);
        return itemDataList;
    });

    private MonsterDataList monsterDataList;
    public MonsterDataList MonsterDB => monsterDataList ??= LoadData<MonsterDataList, MetaMonsterDataList>("MonsterData", data =>
    {
        monsterDataList = new MonsterDataList();
        monsterDataList.SetData(data.MonsterData);
        return monsterDataList;

    });

    private PlayerDataList playerDataList;
    public PlayerDataList PlayerDB => playerDataList ??= LoadData<PlayerDataList, PlayerDataList>("Player", data =>
    {
        playerDataList = new PlayerDataList();
        playerDataList.SetData(data.Player);
        return playerDataList;
    });


    private DungeonDataList dungeonDataList;
    public DungeonDataList DungeonDB => dungeonDataList ??= LoadData<DungeonDataList, DungeonDataList>("Dungeon", data =>
    {
        dungeonDataList = new DungeonDataList();
        dungeonDataList.SetData(data.Dungeon);
        return dungeonDataList;
    });

    protected override void Awake()
    {
        base.Awake();
        LoadAllData();
    }

    private void LoadAllData()
    {
        var items = ItemDB;
        var monsters = MonsterDB;
        var players = PlayerDB;
        var dungeons = DungeonDB;
    }


    // 제너릭 메서드: JSON 데이터 로드 및 변환
    private T LoadData<T, M>(string resourceKey, System.Func<M, T> customAction = null) where T : new()
    {
        TextAsset data = ResourceManager.Instance.LoadAsset<TextAsset>(resourceKey, eAssetType.JsonData);
        if (data != null)
        {
            M MetaDataList = JsonUtility.FromJson<M>(data.text);
            var loadData = customAction.Invoke(MetaDataList);
            return loadData;
        }
        else
        {
            return default;
        }
    }
}
