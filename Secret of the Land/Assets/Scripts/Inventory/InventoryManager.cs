using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public Inventory bag;
    public SlotController slotPrefab;
    public GameObject grid;
    public GameObject inventoryPanel;
    public TextMeshProUGUI itemInfo;
    public TextMeshProUGUI ItemInfo => itemInfo;
    public SlotController currentSlot;
    private const int maxSlots = 10;
    private int slotCount = 0;
    private bool isOpen = false;
    
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of InventoryManager found!");
            return;
        }
        instance = this;
        itemInfo.text = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            OpenCloseInventory();
        }
    }

    public static void CreateNewItem(GameItem item)
    {
        if (instance.slotCount >= maxSlots)
        {
            return;
        }
        ++instance.slotCount;
        SlotController newItem = Instantiate(instance.slotPrefab, instance.grid.transform.position, Quaternion.identity);
        newItem.transform.SetParent(instance.grid.transform);
        newItem.item = item;
        newItem.image.sprite = item.item.itemImage;
        newItem.countText.text = "x" + item.item.count.ToString();
    }

    public static void Refresh(GameItem item)
    {
        foreach (var child in instance.grid.GetComponentsInChildren<SlotController>())
        {
            if (child.item.item.itemName == item.item.itemName)
            {
                var countText = child.countText.text;
                int count = int.Parse(countText[1..]) + 1;
                child.countText.text = "x" + count.ToString();
            }
        }
    }

    public void UseItem()
    {
        if (instance.currentSlot == null)
        {
            return;
        }

        if (instance.currentSlot.item.item.itemType == ItemType.Consumable)
        {
            // if (instance.currentSlot.item.onItemUse == null)
            // {
            //     Debug.Log("No use action found for this item");
            // }
            instance.currentSlot.item.onItemUse?.Invoke(instance.currentSlot.item.item.value);
            instance.currentSlot.item.item.count--;
            instance.currentSlot.countText.text = "x" + instance.currentSlot.item.item.count.ToString();
        }

        if (instance.currentSlot.item.item.count <= 0)
        {
            Destroy(instance.currentSlot.gameObject);
            instance.slotCount--;
            bag.items.Remove(instance.currentSlot.item);
        }

    }

    public static void UpdateItemInfo(string itemDescription)
    {
        instance.itemInfo.text = itemDescription;
    }

    public void OpenCloseInventory()
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);
    }
}