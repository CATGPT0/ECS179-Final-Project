using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SlotController : MonoBehaviour
{
    public GameItem item;
    public Image image;
    public TextMeshProUGUI countText;

    void Awake()
    {
        image = GetComponent<Image>();
        countText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnItemClicked()
    {
        InventoryManager.instance.ItemInfo.text = item.item.itemInfo;
        InventoryManager.instance.currentSlot = this;
    }
}
