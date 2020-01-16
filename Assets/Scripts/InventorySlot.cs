
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;
    public Image itemIcon;
    public Button removeButton;

    public void AddItem(Item newItem)
    {
        item = newItem;
        itemIcon.sprite = item.icon;
        itemIcon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        itemIcon.sprite = null;
        itemIcon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButtonClick()
    {
        Inventory.instance.RemoveItem(item);
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }

}
