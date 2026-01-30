using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public TextMeshProUGUI desNameText;
    public TextMeshProUGUI desDescriptionText;
    public Button useButton;

    [Header("Debug / Testing")]
    [Tooltip("Kéo nút Test Add Item vào đây")]
    public Button debugAddButton;
    [Tooltip("Chọn loại Item muốn test thêm vào")]
    public ItemType itemTestType = ItemType.ProteinBar;

    private List<Item> inventoryItems = new List<Item>();
    private Item currentSelectedItem;

    private void Start()
    {
        // Setup Description Panel
        if (descriptionPanel) descriptionPanel.SetActive(false);
        if (useButton) useButton.onClick.AddListener(UseCurrentItem);

        // Setup Debug Button [MỚI]
        if (debugAddButton != null)
        {
            debugAddButton.onClick.AddListener(AddTestItem);
        }
    }

    // Hàm nhận Item từ Slot Machine thông qua Connector
    public void AddItemFromSlotMachine(ItemType type)
    {
        if (type == ItemType.None) return;

        // Khởi tạo Class Item
        Item newItem = new Item(type, itemDataBase);

        // Kiểm tra an toàn: nếu DataBase không có item này thì không thêm
        if (newItem.itemSO == null)
        {
            Debug.LogWarning($"Không tìm thấy dữ liệu cho {type}, hủy thêm.");
            return;
        }

        inventoryItems.Add(newItem);
        RefreshUI();
    }

    // Hàm Debug: Được gọi khi ấn nút Add Item [MỚI]
    private void AddTestItem()
    {
        Debug.Log($"<color=green>[Debug]</color> Đang thêm thủ công: {itemTestType}");
        AddItemFromSlotMachine(itemTestType);
    }

    public void RefreshUI()
    {
        // Xóa các Slot cũ
        foreach (Transform child in itemsGridContainer)
        {
            Destroy(child.gameObject);
        }

        // Tạo Slot mới
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
        if (descriptionPanel) descriptionPanel.SetActive(true);

        if (item.itemSO != null)
        {
            desIcon.sprite = item.itemSO.sprite;
            desNameText.text = item.itemSO.Name;
            desDescriptionText.text = item.itemSO.Description;
        }
    }

    public void UseCurrentItem()
    {
        if (currentSelectedItem == null) return;

        Debug.Log("<color=yellow>Using Item: </color>" + currentSelectedItem.itemType.ToString());

        inventoryItems.Remove(currentSelectedItem);

        if (descriptionPanel) descriptionPanel.SetActive(false);
        currentSelectedItem = null;

        RefreshUI();
    }
}