using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    //Item informations
    new public string name      = "New Item";
    public string decription    = "New Description";
    public int maxStack         = 1;
    public bool consumable      = false;

    //Item graphics
    public Sprite icon = null;

    public virtual void Use()
    {
    }
}

[System.Serializable]
public class ItemStack
{
    [SerializeField]
    private Item item;
    [SerializeField]
    private int quantity;

    public Item Item
    {
        get { return item; }
        private set { item = value; }
    }

    public int Quantity
    {
        get { return quantity; }
        private set { quantity = value; }
    }

    public ItemStack() { }
    public ItemStack(Item item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }

    public int Add(int quantity)
    {
        if (quantity <= 0) return 0;

        int available = Item.maxStack - Quantity;
        int added = Mathf.Min(quantity, available);
        Quantity += added;
        Debug.Log($"Added {added} items to stack");
        return added;
    }

    public int Remove(int quantity)
    {
        if (quantity <= 0) return 0;

        int available = Quantity;
        int removed = Mathf.Min(quantity, available);
        Quantity -= removed;
        return removed;
    }

    public bool IsEmpty()
    {
        return Quantity == 0;
    }

    public bool IsFull()
    {
        return Item != null && Quantity == Item.maxStack;
    }
}