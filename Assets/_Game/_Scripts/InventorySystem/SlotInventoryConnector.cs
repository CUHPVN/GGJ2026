using UnityEngine;

public class SlotInventoryConnector : MonoBehaviour
{
    [Header("Connection")]
    public SlotMachine slotMachine;
    public InventoryManager inventoryManager;

    private void OnEnable()
    {
        if (slotMachine != null)
        {
            // Đăng ký nhận sự kiện ID (int)
            slotMachine.PullResult += OnReceiveSlotResult;
        }
    }

    private void OnDisable()
    {
        if (slotMachine != null)
        {
            slotMachine.PullResult -= OnReceiveSlotResult;
        }
    }

    // Hàm này giờ nhận vào int (ID của symbol)
    private void OnReceiveSlotResult(int symbolId)
    {
        // Ép kiểu trực tiếp từ int sang ItemType
        // Ví dụ: symbolId = 1 -> ItemType.ProteinBar
        ItemType type = (ItemType)symbolId;

        // Gửi sang Inventory
        inventoryManager.AddItemFromSlotMachine(type);

        Debug.Log($"<color=cyan>[Connector]</color> Nhận ID: {symbolId} -> Thêm Item: {type}");
    }
}