using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/ItemDataBase")]
public class ItemDataBase : ScriptableObject
{
    [System.Serializable]
    public struct ItemBind
    {
        public ItemType itemType;
        public ItemSO itemSO;
    }
    public ItemBind[] itemBinds;
    public ItemSO GetItemSOByType(ItemType type)
    {
        foreach(ItemBind bind in itemBinds)
        {
            if(bind.itemType == type) return bind.itemSO;
        }
        Debug.LogWarning("No "+type.ToString()+ " ItemSO was Binded!");
        return null;
    }
}
