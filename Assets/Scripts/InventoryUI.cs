using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{

    Inventory inventory;
    public Transform itemsParent;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryUI;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.itemChanged += UpdateUI;
        inventorySlots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void UpdateUI()
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                inventorySlots[i].AddItem(inventory.items[i]);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
        }
        print("update UI");
    }
}
