using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Nếu dùng Text thường thì đổi thành using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Data References")]
    public ItemDataBase itemDataBase;

    [Header("UI Prefabs")]
    public GameObject itemSlotPrefab;
    public Transform itemsGridContainer;

    [Header("Description Panel")]
    public GameObject descriptionPanel;
    public Image desIcon;
    public TextMeshProUGUI desNameText; // Đổi thành Text nếu không dùng TMP
    public TextMeshProUGUI desDescriptionText; // Đổi thành Text nếu không dùng TMP
    public Button useButton;

    private List<Item> inventoryItems = new List<Item>();
    private Item currentSelectedItem;

    private void Start()
    {
        descriptionPanel.SetActive(false);
        useButton.onClick.AddListener(UseCurrentItem);
    }

    // Hàm nhận Item từ Slot Machine thông qua Connector
    public void AddItemFromSlotMachine(ItemType type)
    {
        if (type == ItemType.None) return;

        // Khởi tạo Class Item theo cấu trúc của bạn
        Item newItem = new Item(type, itemDataBase);
        inventoryItems.Add(newItem);

        RefreshUI();
    }

    public void RefreshUI()
    {
        // Xóa các Slot cũ trên UI
        foreach (Transform child in itemsGridContainer)
        {
            Destroy(child.gameObject);
        }

        // Tạo lại các Slot mới
        foreach (Item item in inventoryItems)
        {
            GameObject slotObj = Instantiate(itemSlotPrefab, itemsGridContainer);
            InventorySlot slotScript = slotObj.GetComponent<InventorySlot>();
            slotScript.Setup(item, this);
        }
    }

    public void ShowSelectedItem(Item item)
    {
        currentSelectedItem = item;
        descriptionPanel.SetActive(true);

        desIcon.sprite = item.itemSO.sprite;
        desNameText.text = item.itemSO.Name;
        desDescriptionText.text = item.itemSO.Description;
    }

    public void UseCurrentItem()
    {
        if (currentSelectedItem == null) return;

        //In ra ItemType và xóa Item
        Debug.Log("<color=yellow>Using Item: </color>" + currentSelectedItem.itemType.ToString());

        inventoryItems.Remove(currentSelectedItem);
        descriptionPanel.SetActive(false);
        currentSelectedItem = null;

        RefreshUI();
    }
}