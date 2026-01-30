using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    public ItemType ItemType=ItemType.None;
    public Sprite sprite;
    public string Name="Custom Item Name";
    public string Description="Example..";
}
