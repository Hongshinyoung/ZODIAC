using System.Collections.Generic;
using UnityEngine;

public class UIInventory : UIBase
{
    private List<UIItemSlot> itemSlots;

    private void Awake()
    {
        itemSlots = new List<UIItemSlot>(transform.childCount);

        for (int i = 0; i < transform.childCount; i++)
        {
            UIItemSlot slot = transform.GetChild(i).GetComponent<UIItemSlot>();
            itemSlots.Add(slot);
        }

        SetUI(ItemManager.Instance.Inventory);
        DontDestroyOnLoad(GetComponentInParent<Canvas>().gameObject);
    }

    private void OnEnable()
    {
        UpdateUI(ItemManager.Instance.Inventory);
    }

    private void Start()
    {
        gameObject.SetActive(toggle);
    }

    public void SetUI(Inventory inventory)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            inventory.ItemSlots[i].SetUI(itemSlots[i]);
        }
    }

    public void UpdateUI(Inventory inventory)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            itemSlots[i].UpdateUI(inventory.ItemSlots[i]);
        }
    }

    public override void ToggleUI()
    {
        UpdateUI(ItemManager.Instance.Inventory);
        UIManager.Instance.ToggleUI(this);
    }
}
