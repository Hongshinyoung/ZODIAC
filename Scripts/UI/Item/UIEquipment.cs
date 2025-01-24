using System.Collections.Generic;
using UnityEngine;

public class UIEquipment : UIBase
{
    public Dictionary<string, UIItemSlot> itemSlots { get; private set; }

    private void Awake()
    {
        itemSlots = new Dictionary<string, UIItemSlot>(this.transform.childCount);

        for (int i = 0; i < this.transform.childCount; i++)
        {
            UIItemSlot slot = this.transform.GetChild(i).GetComponent<UIItemSlot>();
            string key = ((EquipmentItemType)(i+1)).ToString();
            Debug.Log((EquipmentItemType)(i+1));

            if (slot != null)
            {
                itemSlots.Add(key, slot);
            }
        }

        SetUI(ItemManager.Instance.Equipment);
        DontDestroyOnLoad(GetComponentInParent<Canvas>().gameObject);
    }

    private void Start()
    {
        gameObject.SetActive(toggle);
    }

    private void OnEnable()
    {
        UpdateUI(ItemManager.Instance.Equipment);
    }

    public void SetUI(Equipment equipment)
    {
        foreach (var key in equipment.ItemSlots.Keys)
        {
            equipment.ItemSlots[key].SetUI(itemSlots[key]);
        }
    }

    public void UpdateUI(Equipment equipment)
    {
        foreach (var key in equipment.ItemSlots.Keys)
        {
            itemSlots[key].UpdateUI(equipment.ItemSlots[key]);
        }
    }

    public override void ToggleUI()
    {
        UpdateUI(ItemManager.Instance.Equipment);
        UIManager.Instance.ToggleUI(this);
    }
}
