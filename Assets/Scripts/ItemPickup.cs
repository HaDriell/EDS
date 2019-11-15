using UnityEngine;

public class ItemPickup : Interactable
{

    public Item item;
     
    public override void OnInteract()
    {
        base.OnInteract();
    }

    void PickUp()
    {
        if (Inventory.instance.HasSpace(item))
        {
            Inventory.instance.Add(item);
            Destroy(gameObject);
        }
    }
}
