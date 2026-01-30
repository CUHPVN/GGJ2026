using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Data")]
    public ShopItemSO[] allItems;
    private int currentCoin;

    [Header("UI References")]
    public TextMeshProUGUI coinText;
    public Transform contentPanel;
    public GameObject itemPrefab;

    void Start()
    {
        // Load số coin từ PlayerPrefs (mặc định là 0 nếu chưa có)
        currentCoin = PlayerPrefs.GetInt("PlayerCoin", 100);
        UpdateCoinUI();
        PopulateShop();
    }

    void PopulateShop()
    {
        foreach (var item in allItems)
        {
            GameObject obj = Instantiate(itemPrefab, contentPanel);
            ShopItemUI ui = obj.GetComponent<ShopItemUI>();

            // Kiểm tra xem item đã được mua chưa
            bool isPurchased = PlayerPrefs.GetInt(item.id, 0) == 1;
            ui.Setup(item, isPurchased);

            // Gắn sự kiện Click nút mua
            ui.buyButton.onClick.AddListener(() => TryBuyItem(item, ui));
        }
    }

    public void TryBuyItem(ShopItemSO item, ShopItemUI ui)
    {
        if (currentCoin >= item.price && PlayerPrefs.GetInt(item.id, 0) == 0)
        {
            currentCoin -= item.price;
            PlayerPrefs.SetInt("PlayerCoin", currentCoin);
            PlayerPrefs.SetInt(item.id, 1); // Đánh dấu đã mua
            PlayerPrefs.Save();

            UpdateCoinUI();
            ui.SetPurchased();
            Debug.Log($"Đã mua thành công: {item.itemName}");
        }
        else
        {
            Debug.Log("Không đủ tiền hoặc đã sở hữu!");
        }
    }

    void UpdateCoinUI()
    {
        coinText.text = "Coins: " + currentCoin.ToString();
    }
}