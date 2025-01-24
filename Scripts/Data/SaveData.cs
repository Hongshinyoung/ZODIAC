using System.Collections.Generic;
[System.Serializable]
public class SaveData : DataBase<SaveData>
{
    public string PlayerName;
    public int Level;
    public int Experience;
    public List<string> Inventory = new List<string>();
    public List<string> Equipment = new List<string>();
    public ClearData ClearData;
    public override void SetData(SaveData metaData)
    {
        this.PlayerName = metaData.PlayerName;
        this.Level = metaData.Level;
        this.Experience = metaData.Experience;
        this.Inventory = metaData.Inventory;
        this.Equipment = metaData.Equipment;
        this.ClearData = metaData.ClearData;
    }
}
[System.Serializable]
public class ClearData
{
    public bool[,] stageCleared = new bool[12, 6]; //12개의 별자리, 6개의 스테이지
}



