using System;
using UnityEngine;

public class ItemPickUp : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();
        PickUpItem();
    }

    private void PickUpItem()
    {
        print("picking up " + item.itemName);
        Inventory.instance.AddItem(item);
        Destroy(gameObject);
    }
}
