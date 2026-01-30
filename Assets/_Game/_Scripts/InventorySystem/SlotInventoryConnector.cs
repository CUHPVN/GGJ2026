using UnityEngine;
using System.Collections.Generic;

public class SlotInventoryConnector : MonoBehaviour
{
    [Header("Connection")]
    public SlotMachine slotMachine;
    public InventoryManager inventoryManager;

    [System.Serializable]
    public struct SpriteToItemMap
    {
        public string spriteName; // Tên của file Sprite trong SlotMachine.sprites
        public ItemType itemType; // Loại Item tương ứng trong Enum
    }

    [Header("Settings")]
    public List<SpriteToItemMap> mappingTable;

    private void OnEnable()
    {
        // Đăng ký sự kiện từ SlotMachine
        if (slotMachine != null)
            slotMachine.PullResult += OnReceiveSlotResult;
    }

    private void OnDisable()
    {
        if (slotMachine != null)
            slotMachine.PullResult -= OnReceiveSlotResult;
    }

    private void OnReceiveSlotResult(string spriteName)
    {
        // Tìm ItemType tương ứng với tên Sprite vừa nhận được
        foreach (var map in mappingTable)
        {
            if (map.spriteName == spriteName)
            {
                inventoryManager.AddItemFromSlotMachine(map.itemType);
                return;
            }
        }
        Debug.LogWarning("Connector: Không tìm thấy mapping cho Sprite tên " + spriteName);
    }
}