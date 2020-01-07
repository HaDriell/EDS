using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private int slot = 0;

    [SerializeField]
    private Image image;

    [SerializeField]
    private Text count;

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        if (inventory != null) UpdateUI();
    }

    private void UpdateUI()
    {
        image.sprite = null;
        count.text = "";

        if (inventory == null) return;
        ItemStack stack = inventory.GetItemStack(slot);
        if (stack == null) return;

        image.sprite = stack.Item.icon;
        count.text = stack.Quantity.ToString();
    }

    private void OnInventorySlotChanged(object publishedEvent)
    {
        InventorySlotChangedEvent e = publishedEvent as InventorySlotChangedEvent;
        if (e.Slot == slot)
        {
            UpdateUI();
        }
    }

    private void Start()
    {
        PubSub.AddListener<InventorySlotChangedEvent>(OnInventorySlotChanged);
        UpdateUI();
    }

    private void OnDestroy()
    {
        PubSub.RemoveListener<InventorySlotChangedEvent>(OnInventorySlotChanged);
    }
}
