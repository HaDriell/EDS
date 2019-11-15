using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple Inventories are tried to be instanciated !");
            return;
        }
        instance = this;
    }
    #endregion //Singleton

    bool init = false;

    public void LateUpdate()
    {
        if (init) return;
        init = true;

        Debug.Log("Remove Placeholders initialization later");
        Item a = ScriptableObject.CreateInstance<Item>();
        a.name = "a";
        Add(a);
        Item b = ScriptableObject.CreateInstance<Item>();
        b.name = "b";
        Add(b);
    }

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;
    public List<Item> items = new List<Item>();

    public bool HasSpace(Item item)
    {
        return items.Count < space;
    }

    public void Add(Item item)
    {
        if (!HasSpace(item))
            return;
        if (item.isDefaultItem)
            return;

        items.Add(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }
}
