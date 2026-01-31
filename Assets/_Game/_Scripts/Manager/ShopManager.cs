using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI totalCoinText;

    void Start()
    {
        UpdateTotalCoinUI();
    }
    public void UpdateTotalCoinUI()
    {
        int newCoins = PlayerPrefs.GetInt("PlayerCoin", 100);

        // Lấy số tiền cũ đang hiển thị từ Text (nếu có)
        string cleanString = totalCoinText.text.Replace("Coins: ", "");
        int oldCoins = int.TryParse(cleanString, out int result) ? result : 0;

        // Chạy hiệu ứng số tiền
        StopAllCoroutines();
        StartCoroutine(UIEffects.NumberTicker(totalCoinText, oldCoins, newCoins, 0.5f));
    }

    // Gọi rung khi mua trượt
    public void OnBuyFailed(RectTransform itemRect)
    {
        StartCoroutine(UIEffects.Shake(itemRect, 0.2f, 10f));
    }
}