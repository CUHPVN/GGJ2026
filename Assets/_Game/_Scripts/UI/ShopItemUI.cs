using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemUI : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;
    public Button buyButton;
    public TextMeshProUGUI buttonText;

    public void Setup(ShopItemSO item, bool isPurchased)
    {
        iconImage.sprite = item.icon;
        nameText.text = item.itemName;
        priceText.text = item.price + " Coins";

        if (isPurchased) SetPurchased();
    }

    public void SetPurchased()
    {
        buttonText.text = "Owned";
        buyButton.interactable = false;
        priceText.text = "";
    }
}