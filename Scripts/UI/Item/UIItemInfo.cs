using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemInfo : UIBase
{
    private UIItemSlot uiItemSlot;
    public ItemData itemData { get; private set; }

    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI typeText;
    [SerializeField] TextMeshProUGUI statText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Button equipBtn;
    [SerializeField] Button unEquipBtn;
    [SerializeField] Button checkBtn;
    [SerializeField] Button cancelBtn;

    private void Awake()
    {
        equipBtn.gameObject.SetActive(false);
        unEquipBtn.gameObject.SetActive(false);
        cancelBtn.gameObject.SetActive(false);
        checkBtn.gameObject.SetActive(false);
    }

    public void SetItem(ItemData data, UIItemSlot slot)
    {
        uiItemSlot = slot;
        itemData = data;
        image.sprite = data.sprite;
        nameText.text = data.name;
        typeText.text = data.itemType.ToString();
        statText.text = data.stat.ToString();
        descriptionText.text = data.description;

        if(itemData.itemType == ItemType.None)
        {
            checkBtn.gameObject.SetActive(true);
        }
        else if(uiItemSlot.Type == SlotType.Inventory)
        {
            equipBtn.gameObject.SetActive(true);
            cancelBtn.gameObject.SetActive(true);
        }
        else if (uiItemSlot.Type == SlotType.Equipment)
        {
            unEquipBtn.gameObject.SetActive(true);
            cancelBtn.gameObject.SetActive(true);
        }
    }

    public void OnClickEquipBtn()
    {
        if (itemData.itemType != ItemType.None)
        {
            ItemSlot equipmentSlot = ItemManager.Instance.Equipment.ItemSlots[(itemData.itemType).ToString()];
            if(equipmentSlot.ItemData != null)
            {
                for (int i = 0; i < equipmentSlot.ItemIndex; i++)
                {
                    ItemManager.Instance.Inventory.StackItem(equipmentSlot.ItemData);
                }
                ItemManager.Instance.Equipment.RemoveSlotItem(equipmentSlot.ItemData);
                equipmentSlot.UpdateUI();
            }

            for (int i = 0; i < uiItemSlot.ItemSlot.ItemIndex; i++)
            {
                ItemManager.Instance.Equipment.SetSlotItem(itemData);
            }
            ItemManager.Instance.Inventory.RemoveItem(itemData);
        }
        else
        {
            Debug.Log("뭔가 문제가 있다");
        }

        equipBtn.gameObject.SetActive(false);
        cancelBtn.gameObject.SetActive(false);
        UIManager.Instance.Hide<UIItemInfo>();
    }

    public void OnClickUnEquipBtn()
    {
        for (int i = 0; i < uiItemSlot.ItemSlot.ItemIndex; i++)
        {
            ItemManager.Instance.Inventory.StackItem(itemData);
        }
        ItemManager.Instance.Equipment.RemoveSlotItem(itemData);

        unEquipBtn.gameObject.SetActive(false);
        cancelBtn.gameObject.SetActive(false);
        UIManager.Instance.Hide<UIItemInfo>();
    }

    public void OnClickCancelBtn()
    {
        equipBtn.gameObject.SetActive(false);
        cancelBtn.gameObject.SetActive(false);
        UIManager.Instance.Hide<UIItemInfo>();
    }

    public void OnClickCheackBtn()
    {
        checkBtn.gameObject.SetActive(false);
        UIManager.Instance.Hide<UIItemInfo>();
    }
}
