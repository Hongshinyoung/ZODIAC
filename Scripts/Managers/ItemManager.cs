public class ItemManager : Singleton<ItemManager>
{
    protected override void Awake()
    {
        base.Awake();
        inventory = gameObject.AddComponent<Inventory>();
        equipment = gameObject.AddComponent<Equipment>();
    }

    private Inventory inventory;
    public Inventory Inventory
    {
        get
        {
            return inventory;
        }

        set
        {
            inventory = value;
        }
    }

    private Equipment equipment;
    public Equipment Equipment
    {
        get
        {
            return equipment;
        }

        set
        {
            equipment = value;
        }
    }
}
