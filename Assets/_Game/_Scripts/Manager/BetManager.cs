using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetManager : PersistentSingleton<BetManager>
{
    [Header("Stats")]
    [SerializeField] private int playerCoin = 0;
    [SerializeField] private int playerHeart = 10;
    [SerializeField] private int mobHeart = 10;
    [SerializeField] TextMeshProUGUI panelText;
    [SerializeField] TextMeshProUGUI playerHeartText;
    [SerializeField] TextMeshProUGUI playerCoinText;
    [SerializeField] TextMeshProUGUI betAmmount;
    [SerializeField] bool isPlayerWin = true;

    private int initialMobHeart;
    private int currentRoundBet; // Mức cược tối thiểu cần theo trong lượt này
    private int difference = 0;

    // Lưu trữ: (số heart đã cược trong lượt, người chơi có cược nhiều hơn quái không)
    private List<(int betVal, bool isPlayerBetMore)> betHistory = new List<(int, bool)>();

    protected override void Awake()
    {
        base.Awake();
        initialMobHeart = mobHeart;
    }

    /// <summary>
    /// Lượt đầu tiên: Người chơi chủ động đưa ra mức cược.
    /// </summary>
    public void StartFirstBet(int playerInitiatedVal)
    {
        betHistory.Clear();
        // Quái bắt buộc đặt bằng hoặc hơn (ở đây mặc định quái đặt bằng để khởi đầu)
        ProcessBet(playerInitiatedVal, false);
        UpdateHeartAndCoin();
        panelText.text = $"Lượt đầu: Bạn và quái cùng cược {playerInitiatedVal}";
    }

    /// <summary>
    /// Các lượt tiếp theo: Quái đưa ra mức cược random.
    /// </summary>
    public void MobProposeBet()
    {
        int maxPossible = Mathf.Min(playerHeart, mobHeart);
        if (maxPossible <= 0) { BetEnd(isPlayerWin); return; }

        // Logic: Tỉ lệ cược thấp cao hơn cược cao (dùng Weighted Random hoặc AnimationCurve)
        // Ở đây dùng một công thức đơn giản: Bình phương một số từ 0-1 để kéo kết quả về phía nhỏ
        float randomWeight = Random.value;
        currentRoundBet = Mathf.CeilToInt(Mathf.Pow(randomWeight, 2) * maxPossible);
        if (currentRoundBet == 0) currentRoundBet = 1;

        panelText.text=($"Quái đề nghị mức cược: {currentRoundBet}. Bạn có theo (BetMore) hay Bỏ cuộc (Surrender)?");
        UpdateBetAmmount();
    }

    public void PlayerDecision()
    {
       // if (acceptAndRaise)
        {
            // Người chơi chọn đặt cược nhiều hơn hoặc bằng
            // Giả sử "nhiều hơn" ở đây là +1 hoặc người chơi tự nhập (ở đây tôi ví dụ là bằng)
            currentRoundBet += difference;
            ProcessBet(currentRoundBet, difference > 0);
            difference = 0;

            // Kiểm tra điều kiện kết thúc sớm
            if (playerHeart <= 0 || mobHeart <= 0) BetEnd(isPlayerWin);
            else MobProposeBet(); // Tiếp tục lượt mới
        }
        //else
        //{
        //    Surrender();
        //}
    }

    private void ProcessBet(int val, bool isPlayerMore)
    {
        int finalVal = Mathf.Min(val, playerHeart, mobHeart);
        playerHeart -= finalVal;
        mobHeart -= finalVal;
        UpdateHeartAndCoin();
        betHistory.Add((finalVal, isPlayerMore));
    }

    public void Surrender()
    {
        panelText.text=("Bạn đã bỏ cuộc! Mất toàn bộ số Heart đã cược.");
        // Chuyển màn mới mà không nhận lại Heart/Coin
        TransitionToNextLevel();
    }

    public void BetEnd(bool isPlayerWin)
    {
        if (isPlayerWin)
        {
            int totalHeartRefund = 0;
            int bonusCoin = 0;

            foreach (var record in betHistory)
            {
                totalHeartRefund += record.betVal;
                if (record.isPlayerBetMore)
                {
                    bonusCoin += record.betVal;
                }
            }

            playerHeart += totalHeartRefund; // Hoàn trả heart
            playerCoin += (bonusCoin + initialMobHeart); // Thưởng coin
            UpdateHeartAndCoin();

            panelText.text = ($"Thắng! Nhận lại {totalHeartRefund} Heart và {bonusCoin + initialMobHeart} Coin");
        }
        else
        {
            panelText.text = "You lose";
        }

            TransitionToNextLevel();
    }

    private void TransitionToNextLevel()
    {
        // Logic chuyển màn ở đây
    }

    public void BetMore()
    {
        if(difference<playerHeart)
            difference++;
        UpdateBetAmmount();
    }
    private void UpdateHeartAndCoin()
    {
        playerCoinText.text = $"Coin: {playerCoin}";
        playerHeartText.text = $"Heart: {playerHeart}";
    }
    private void UpdateBetAmmount()
    {
        betAmmount.text = $"Bet Ammount: {currentRoundBet + difference}";
    }
}