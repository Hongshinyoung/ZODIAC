using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : UIBase
{
    public ItemSlot ItemSlot { get; private set; }
    public ItemData ItemData { get; private set; }
    [SerializeField] public SlotType Type { get; private set; }

    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemAmountText;
    private Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;

        var uiInventory = GetComponentInParent<UIInventory>();
        var uiEquipment = GetComponentInParent<UIEquipment>();

        if (uiInventory != null)
        {
            Type = SlotType.Inventory;
        }
        else if (uiEquipment != null)
        {
            Type = SlotType.Equipment;
        }

        itemImage.enabled = false;
        itemAmountText.enabled = false;
    }

    public void UpdateUI(ItemSlot slot)
    {
        ItemSlot = slot;
        ItemData = slot.ItemData;
        itemImage.enabled = slot.ItemExistense;
        itemImage.sprite = slot.itemImage;
        if((ItemData != null) && (ItemData.itemType == ItemType.Potion))
        {
            itemAmountText.enabled = slot.ItemExistense;
            itemAmountText.text = slot.ItemIndex.ToString();
        }
    }

    public void ClearSlot()
    {
        ItemSlot = null;
        ItemData = null;
        itemImage.sprite = null;
        itemImage.enabled = false;
        itemAmountText.text = "0";
        itemAmountText.enabled = false;
    }

    public void OnClickItemSlot()
    {
        if(ItemData == null)
        {
            return;
        }
        UIManager.Instance.Show<UIItemInfo>();
        UIItemInfo info = UIManager.Instance.Get<UIItemInfo>();
        info.SetItem(ItemData, this);
    }
}
