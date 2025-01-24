using UnityEngine;

public class Item : MonoBehaviour
{
    private ItemData data;

    private void Awake()
    {
        data = new ItemData();

        int rand = UnityEngine.Random.Range(1, 4);
        string key = $"ITEM{rand:D3}";
        data = DataManager.Instance.ItemDB.LoadData(key);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            ItemManager.Instance.Inventory.StackItem(data);
            Destroy(gameObject);
            Debug.Log("아이템 획득! : " + data.korName);

            foreach(var item in ItemManager.Instance.Inventory.ItemSlots)
            {
                if (item != null)
                {
                    Debug.Log(item.name);
                }
            }
        }
    }
}
