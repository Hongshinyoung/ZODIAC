using UnityEngine;

public enum SlotType
{
    None,
    Inventory,
    Equipment
}

public class ItemSlot : MonoBehaviour
{
    public ItemData ItemData { get; private set; }
    public int ItemIndex { get; private set; } = 0;
    public bool ItemExistense { get; private set; } = false;

    public Sprite itemImage;
    [SerializeField] public SlotType Type { get; private set; }

    public UIItemSlot uiItemSlot { get; private set; }


    private void Awake()
    {
        var inventory = GetComponentInParent<Inventory>();
        var equipment = GetComponentInParent<Equipment>();

        if (inventory != null)
        {
            Type = SlotType.Inventory;
        }
        else if (equipment != null)
        {
            Type = SlotType.Equipment;
        }
    }

    public void ClearSlot()
    {
        ItemData = null;
        ItemIndex = 0;
        ItemExistense = false;
        itemImage = null;
        uiItemSlot.ClearSlot();
    }

    public void SetUI(UIItemSlot ui)
    {
        uiItemSlot = ui;
    }

    public void UpdateUI()
    {
        if (uiItemSlot != null)
        {
            uiItemSlot.UpdateUI(this);
        }
        else
        {
            Debug.Log("아이템 슬롯 업데이트 했더니 슬롯의 ui가 null인 상태");
        }
    }

    public void SetItem(ItemData getItem)
    {
        ItemData = getItem;
        AddItem();
        ItemExistense = true;
        itemImage = getItem.sprite;
    }

    public void AddItem()
    {
        ItemIndex++;
    }

    public void UseItem()
    {
        if ((this.ItemData.itemType == ItemType.Potion) && (ItemIndex > 0))
        {
            ItemIndex--;
            GameManager.Instance.Player.Data.hp += ItemData.stat;
            UpdateUI();
        }

        if (ItemIndex == 0)
        {
            ItemManager.Instance.Equipment.RemoveSlotItem(ItemData);
            UpdateUI();
        }
    }

    public override string ToString()
    {
        return $"ItemSlot";
    }
}
