using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Shop/PowerUp")]
public class ShopItemSO : ScriptableObject
{
    public string itemName;
    public string description;
    public int price;
    public Sprite icon;
    public string id; // Dùng để lưu vào PlayerPrefs (vd: "extra_heart")
}