using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Status
{
    public int attack;
    public int defense;

    public Status(int atk, int def)//(int atk, int def, int str, int crt)
    {
        this.attack = atk;
        this.defense = def;
    }

}

[System.Serializable]
public class ItemData : DataBase<MetaItemData>
{
    public string id;
    public string name;
    public Sprite sprite;
    public string korName;
    public ItemType itemType;
    public int stat;
    public string description;
    public int number;

    public override void SetData(MetaItemData metaItemData)
    {
        id = metaItemData.id;
        name = metaItemData.name;
        sprite = ResourceManager.Instance.LoadAsset<Sprite>($"Item/{metaItemData.imageName}", eAssetType.Sprites);
        korName = metaItemData.korName;
        if (Enum.TryParse(metaItemData.itemType, out itemType))
        {
            // 변환 성공
            Console.WriteLine($"딕셔너리 변환 시 ItemType 변환 성공 아이템 : {korName}");
        }
        else
        {
            // 변환 실패
            Console.WriteLine($"딕셔너리 변환 시 ItemType 변환 실패 아이템 : {korName}");
        }
        stat = metaItemData.stat;
        description = metaItemData.description;
        number = metaItemData.number;
    }
}

[System.Serializable]
public class ItemDataList : DataBaseList<string, ItemData, MetaItemData> // key, 리스트 안에 들어가있는 데이터, 어떤 데이터를 가져올건지
{
    public override void SetData(List<MetaItemData> metaItemDatas)
    {
        datas = new Dictionary<string, ItemData>(metaItemDatas.Count);

        metaItemDatas.ForEach(obj =>
        {
            ItemData item = new ItemData();
            item.SetData(obj);
            datas.Add(item.id, item);
        });
    }

    // 딕셔너리를 리스트 형태로 변환 (저장용)
    public List<ItemData> ToList()
    {
        return new List<ItemData>(datas.Values);
    }

    // 리스트 데이터를 딕셔너리로 복원 (불러오기용)
    public void FromList(List<ItemData> itemList)
    {
        datas = new Dictionary<string, ItemData>(itemList.Count);

        foreach (var item in itemList)
        {
            datas[item.id] = item;
        }
    }

    // 특정 아이템 ID로 데이터를 가져오는 메서드
    public ItemData GetItemById(string id)
    {
        if (datas.TryGetValue(id, out var item))
            return item;
        return null;
    }

}