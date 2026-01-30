using UnityEngine;

public class Item
{
    public ItemType itemType;
    public ItemSO itemSO;
    public Item(ItemType itemType,ItemDataBase itemDataBase)
    {
        this.itemType = itemType;
        itemSO = itemDataBase.GetItemSOByType(itemType);
    }
}
