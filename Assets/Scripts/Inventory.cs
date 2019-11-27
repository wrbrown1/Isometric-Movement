using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public static Inventory instance;
    public delegate void ItemChanged();
    public ItemChanged itemChanged;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of the inventory has been found!");
            return;
        }
        instance = this;
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        if(itemChanged != null)
        {
            itemChanged.Invoke();
        }
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        if (itemChanged != null)
        {
            itemChanged.Invoke();
        }
    }
}
