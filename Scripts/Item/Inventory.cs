using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<ItemSlot> itemSlots;

    public List<ItemSlot> ItemSlots
    {
        get
        {
            if (itemSlots == null)
            {
                itemSlots = new List<ItemSlot>();

                var obj = ResourceManager.Instance.LoadAsset<GameObject>("UIInventory", eAssetType.UI);

                for (int i = 0; i < obj.transform.childCount; i++)
                {
                    ItemSlot slot = new ItemSlot();
                    itemSlots.Add(slot);
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
        itemSlots = new List<ItemSlot>();

        var obj = ResourceManager.Instance.LoadAsset<GameObject>("UIInventory", eAssetType.UI);

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            ItemSlot slot = new ItemSlot();
            itemSlots.Add(slot);
        }
    }

    private void Start()
    {
        UIManager.Instance.Show<UIInventory>();
    }

    public void StackItem(ItemData newItem)
    {
        foreach (ItemSlot slot in itemSlots)
        {
            if (slot.ItemData != null)
            {
                if (IsStackable(newItem) && (slot.ItemData.name == newItem.name))
                {
                    slot.AddItem();
                    UpdateUI();
                    return;
                }
            }
        }

        foreach (ItemSlot slot in itemSlots)
        {
            if (slot.ItemData == null)
            {
                slot.SetItem(newItem);
                UpdateUI();
                return;
            }
        }

        Debug.Log("인벤토리가 꽉 찼습니다.");
    }

    public void RemoveItem(ItemData item)
    {
        foreach (ItemSlot slot in itemSlots)
        {
            if (slot.ItemData != null)
            {
                if (slot.ItemData.itemType == item.itemType)
                {
                    slot.ClearSlot();
                    break;
                }
            }
        }
    }

    public void UpdateUI()
    {
        UIManager.Instance.Get<UIInventory>().UpdateUI(this);
    }

    private bool IsStackable(ItemData newItem)
    {
        return newItem.itemType == ItemType.Potion;
    }

    public List<string> GetInventoryToList()
    {
        List<string> itemIdlist = new List<string>();
       
        foreach (var item in ItemSlots)
        {
            if (item == null)
            {
                if (item.ItemData == null) { continue; }
            }
            Debug.Log(item.name);
            itemIdlist.Add(item.ItemData.id);
        }

        return itemIdlist;
    }
}
