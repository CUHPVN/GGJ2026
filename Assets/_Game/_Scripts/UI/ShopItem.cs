using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [Header("Settings")]
    public string itemID;        // Ví dụ: "HeartUpgrade"
    public int basePrice = 10;   // Giá gốc lượt đầu
    public float multiplier = 1.5f; // Hệ số nhân giá (ví dụ: tăng 50% mỗi cấp)

    [Header("UI Elements")]
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI priceText;
    public Button buyButton;

    [SerializeField] RectTransform rect;

    private void Start()
    {
        RefreshUI();
        buyButton.onClick.AddListener(BuyUpgrade);
       // PlayerPrefs.SetInt("PlayerCoin", 1000);
    }

    public void RefreshUI()
    {
        // Lấy cấp độ hiện tại (mặc định là cấp 0)
        int currentLevel = PlayerPrefs.GetInt(itemID + "_Level", 0);

        // Tính giá hiện tại: Giá gốc * (Hệ số ^ Cấp độ)
        int currentPrice = Mathf.RoundToInt(basePrice * Mathf.Pow(multiplier, currentLevel));

        levelText.text = "Level: " + currentLevel;
        priceText.text = currentPrice + " Coins";

        // Kiểm tra tiền của người chơi để tắt/mở nút
        int playerCoins = PlayerPrefs.GetInt("PlayerCoin", 100);
        //buyButton.interactable = (playerCoins >= currentPrice);
    }

    void BuyUpgrade()
    {
        int currentLevel = PlayerPrefs.GetInt(itemID + "_Level", 0);
        int currentPrice = Mathf.RoundToInt(basePrice * Mathf.Pow(multiplier, currentLevel));
        int playerCoins = PlayerPrefs.GetInt("PlayerCoin", 100);

        if (playerCoins >= currentPrice)
        {
            // Trừ tiền
            playerCoins -= currentPrice;
            PlayerPrefs.SetInt("PlayerCoin", playerCoins);

            // Tăng cấp độ
            PlayerPrefs.SetInt(itemID + "_Level", currentLevel + 1);
            PlayerPrefs.Save();

            // Cập nhật UI
            RefreshUI();
            FindFirstObjectByType<ShopManager>().UpdateTotalCoinUI();

            Debug.Log($"Đã nâng cấp {itemID} lên Level {currentLevel + 1}");
        }
        else
        {
            FindFirstObjectByType<ShopManager>().OnBuyFailed(rect);
        }
    }
}