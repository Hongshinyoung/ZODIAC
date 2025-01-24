using System.Collections.Generic;


[System.Serializable]
public class MetaItemData
{
    public string id;
    public string name;
    public string imageName;
    public string korName;
    public string itemType;
    public int stat;
    public string description;
    public int number;
}

[System.Serializable]
public class MetaItemDataList
{
    public List<MetaItemData> ItemData;
}

