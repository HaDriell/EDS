using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;

    Item item;


    public void SetItem(Item newItem)
    {
        item = newItem;
        //Update the UI accordingly
        if (item != null)
        {
            icon.sprite = item.icon;
            icon.enabled = true;
            removeButton.interactable = true;
        } 
        else
        {
            icon.sprite = null;
            icon.enabled = false;
            removeButton.interactable = false;
        }
    }

    public void OnRemoveButton()
    {
        SetItem(null);
    }

    public void OnUseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
