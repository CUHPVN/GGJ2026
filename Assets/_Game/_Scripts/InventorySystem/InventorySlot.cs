using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image iconImage;
    public Button button;

    private Item _item;
    private InventoryManager _manager;

    public void Setup(Item item, InventoryManager manager)
    {
        _item = item;
        _manager = manager;

        if (_item.itemSO != null)
        {
            iconImage.sprite = _item.itemSO.sprite;
        }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => _manager.ShowSelectedItem(_item));
    }
}