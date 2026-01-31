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
        // Lấy tiền, mặc định 100 nếu lần đầu chơi
        int coins = PlayerPrefs.GetInt("PlayerCoin", 100);
        totalCoinText.text = "Coins: " + coins;
    }
}