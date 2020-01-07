using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOnInteract : MonoBehaviour, IInteractable
{
    [SerializeField]
    public ItemStack stack;

    public void OnEndHover() { }

    public void OnInteract()
    {
        //Collect into Player's Inventory
        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory == null) return;

        int transferedQuantity = inventory.Add(stack.Item, stack.Quantity);
        stack.Remove(transferedQuantity);
        //Destroy if empty
        if (stack.IsEmpty()) Destroy(gameObject);
    }

    public void OnStartHover() { }
}