using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    private Dictionary<string, ItemSlot> itemSlots;

    public Dictionary<string, ItemSlot> ItemSlots
    {
        get
        {
            if (itemSlots == null)
            {
                itemSlots = new Dictionary<string, ItemSlot>();

                var obj = ResourceManager.Instance.LoadAsset<GameObject>("UIEquipment", eAssetType.UI);

                for (int i = 0; i < obj.transform.childCount; i++)
                {
                    ItemSlot slot = new ItemSlot();
                    string key = ((EquipmentItemType)(i + 1)).ToString();
                    itemSlots.Add(key, slot);
                }
            }
            return itemSlots;
        }

        set
        {
            itemSlots = value;
        }
    }

    private void Awake()
    {
        itemSlots = new Dictionary<string, ItemSlot>();

        var obj = ResourceManager.Instance.LoadAsset<GameObject>("UIEquipment", eAssetType.UI);

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            ItemSlot slot = new ItemSlot();
            string key = ((EquipmentItemType)(i + 1)).ToString();
            itemSlots.Add(key, slot);
        }
    }

    private void Start()
    {
        UIManager.Instance.Show<UIEquipment>();
    }

    public void UpdateUI()
    {
        UIManager.Instance.Get<UIEquipment>().UpdateUI(this);
    }

    public void SetSlotItem(ItemData data)
    {
        string key = ((EquipmentItemType)data.itemType).ToString();
        itemSlots[key].SetItem(data);
        UpdateUI();
        GameManager.Instance.Player.StatUpdate(data, true);
    }

    public void RemoveSlotItem(ItemData data)
    {
        string key = ((EquipmentItemType)data.itemType).ToString();
        itemSlots[key].ClearSlot();
        UpdateUI();
        GameManager.Instance.Player.StatUpdate(data, false);
    }

    public List<string> GetEquipmentToList()
    {
        List<string> equipIdlist = new List<string>();

        foreach (var item in ItemSlots)
        {
            if (item.Value == null) { continue; }
            equipIdlist.Add(item.Value.ItemData.id);
        }
        return equipIdlist;
    }
}
