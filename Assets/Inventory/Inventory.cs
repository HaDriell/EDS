using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotChangedEvent
{
    public Inventory Inventory { get; private set; }
    public int Slot { get; private set; }

    public InventorySlotChangedEvent(Inventory inventory, int slot)
    {
        Inventory = inventory;
        Slot = slot;
    }
}

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private ItemStack[] itemStacks = new ItemStack[16];

    /// <summary>
    /// Sums all of the item quantities through the Inventory ItemStacks.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public bool Find(Item item, int quantity = 1)
    {
        int count = 0;
        foreach (ItemStack stack in itemStacks)
        {
            if (stack.Item == item && stack.Quantity > 0)
            {
                count += stack.Quantity;
                if (count >= quantity)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Inserts as many items as it possibly can stack and returns the number of items inserted.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public int Add(Item item, int quantity)
    {
        //Before doing anything, check that added quantity is legal
        if (quantity <= 0) return 0;

        //First, Try to Fill existing Stacks
        int remaining = quantity;
        if (remaining > 0)
        {
            for (int i = 0; i < itemStacks.Length; i++)
            {
                //Look for similar item Stacks only
                if (itemStacks[i] == null || itemStacks[i].Item != item)
                    continue;

                //Fill stack
                int filled = itemStacks[i].Add(remaining);
                if (filled > 0)
                {
                    //Update Remaining
                    remaining -= filled;
                    //Notify Update
                    PubSub.Publish(new InventorySlotChangedEvent(this, i));
                    if (remaining == 0)
                        break;
                }
            }
        }

        //Then try to create new stacks (stop if finished or full)
        if (remaining > 0)
        {
            for (int i = 0; i < itemStacks.Length; i++)
            {
                Debug.Log($"Stack[{i}] : {itemStacks[i] == null}, Empty = {itemStacks[i] == null || itemStacks[i].IsEmpty()}");
                //Look for empty stacks
                if (itemStacks[i] == null || !itemStacks[i].IsEmpty())
                    continue;

                //Create a Stack of this item and fill it
                itemStacks[i] = new ItemStack(item, 0);
                int filled = itemStacks[i].Add(remaining);

                if (filled > 0)
                {
                    //Update Remaining
                    remaining -= filled;
                    //Notify Update
                    PubSub.Publish(new InventorySlotChangedEvent(this, i));
                    if (remaining == 0)
                        break;
                }
            }
        }

        int added = quantity - remaining;
        //MAYDO : notify item addition
        return added;
    }

    public int Remove(Item item, int quantity)
    {
        if (quantity <= 0) return 0;

        //Sequencialy remove items from stacks in
        int remaining = quantity;
        for (int i = 0; i < itemStacks.Length; i++)
        {
            //Filter out non interesting itemStacks
            if (itemStacks[i] == null || itemStacks[i].Item != item)
                continue;

            //Empty the stack
            int removed = itemStacks[i].Remove(remaining);

            if (removed > 0)
            {
                //Update Remaining
                remaining -= removed;
                //Notify Update
                PubSub.Publish(new InventorySlotChangedEvent(this, i));
                if (remaining == 0)
                    break;
            }
        }

        int removedTotal = quantity - remaining;
        //MAYDO : Notify item removal
        return removedTotal;
    }

    public bool IsFull()
    {
        foreach (ItemStack stack in itemStacks)
        {
            if (!stack.IsFull())
                return false;
        }
        return true;
    }

    public bool IsEmpty()
    {
        foreach (ItemStack stack in itemStacks)
        {
            if (!stack.IsEmpty())
                return false;
        }
        return true;
    }
}