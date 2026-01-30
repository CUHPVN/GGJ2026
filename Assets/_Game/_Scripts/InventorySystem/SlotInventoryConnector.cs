using UnityEngine;
using System.Collections.Generic;

public class SlotInventoryConnector : MonoBehaviour
{
    [Header("System Links")]
    public SlotMachine slotMachine;       // Kéo SlotMachine vào đây
    public InventoryManager inventoryManager; // Kéo InventoryManager vào đây

    [Header("Configuration")]
    [Tooltip("Bảng map số ID từ SlotMachine sang ItemType")]
    public List<SlotMap> mappingTable;

    // Struct định nghĩa quy tắc Map
    [System.Serializable]
    public struct SlotMap
    {
        public string description; // Ghi chú (VD: Hình cái búa)
        public int slotSymbolID;   // ID mà SlotMachine trả ra (0, 1, 2...)
        public ItemType itemType;  // Enum tương ứng muốn nhận
    }

    private void OnEnable()
    {
        if (slotMachine != null)
        {
            // Đăng ký nhận kết quả từ SlotMachine
            // Lưu ý: SlotMachine cần có event: public event Action<int> PullResult;
            slotMachine.PullResult += OnReceiveResult;
        }
    }

    private void OnDisable()
    {
        if (slotMachine != null)
        {
            slotMachine.PullResult -= OnReceiveResult;
        }
    }

    // Hàm xử lý chính
    private void OnReceiveResult(int symbolIndex)
    {
        // 1. Duyệt qua bảng Map để tìm ItemType tương ứng với số ID vừa quay ra
        ItemType typeFound = ItemType.None;
        bool isFound = false;

        foreach (var map in mappingTable)
        {
            if (map.slotSymbolID == symbolIndex)
            {
                typeFound = map.itemType;
                isFound = true;
                break;
            }
        }

        // 2. Nếu tìm thấy và khác None -> Gửi sang InventoryManager
        if (isFound && typeFound != ItemType.None)
        {
            inventoryManager.AddItemFromSlotMachine(typeFound);
            Debug.Log($"<color=cyan>[Connector]</color> Slot ID: {symbolIndex} -> Add Item: {typeFound}");
        }
        else
        {
            Debug.LogWarning($"<color=orange>[Connector]</color> Chưa config ItemType cho Slot ID: {symbolIndex}");
        }
    }
}