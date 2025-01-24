using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Player player;
    public Item item;
    public Inventory inventory;
    public Equipment equipment;
    public Player Player
    {
        get
        {
            if(player == null)
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            return player;
        }
        set
        {
            player = value;
        }
    }
    public ItemDataList itemDataList;
    private SaveData currentSaveData;
    public UserData UserData { get; private set; }
    public ClearData clearData { get; set; }

    private DungeonManager dungeonManager;
    public DungeonManager DungeonManager => dungeonManager;
    public PuzzleManager PuzzleManager => dungeonManager?.PuzzleManager;

    protected override void Awake()
    {
        base.Awake();
        if (clearData == null)
        {
            clearData = new ClearData();
        }

        if (UserData == null)
        {
            UserData = new UserData();
        }

        if (currentSaveData == null)
        {
            currentSaveData = new SaveData();
        }
    }

    public void SaveGame()
    {
        if (UserData != null)
        {
            currentSaveData.PlayerName = UserData.name;
            currentSaveData.Level = UserData.level;
            currentSaveData.Experience = UserData.exp;
            currentSaveData.ClearData = clearData;

            currentSaveData.Inventory = ItemManager.Instance.Inventory.GetInventoryToList();
            currentSaveData.Equipment = ItemManager.Instance.Equipment.GetEquipmentToList();

            // 데이터를 저장
            SaveManager.Instance.SaveGame(currentSaveData);  
        }
    }

    public void LoadGame()
    {
        SaveData loadedData = SaveManager.Instance.LoadGame();
        if (loadedData != null)
        {
            currentSaveData = loadedData;
            // SaveData 복원
            UserData.name = currentSaveData.PlayerName;
            UserData.level = currentSaveData.Level;
            UserData.exp = currentSaveData.Experience;
            //inventory.GetInventoryToList(); = currentSaveData.Inventory;
            clearData = currentSaveData.ClearData;

            // 인벤토리 복원
            //itemDataList.FromList(currentSaveData.Inventory);
        }
    }


    public void EnterDungeon(string stageId, int starIndex, int stageIndex)
    {
        SceneLoadManager.Instance.ChangeScene("InGameDungeonScene", () =>
        {
            dungeonManager = new GameObject("DungeonManager").AddComponent<DungeonManager>();
            dungeonManager.InitializeDungeon(stageId, starIndex, stageIndex);
        }, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }


    public void ExitDungeon()
    {
        if (dungeonManager != null) Destroy(dungeonManager.gameObject);
        dungeonManager = null;
    }
}