public enum CombatEventType
{
    OnTurnStart,          // Đầu mỗi lượt (Check Regeneration)
    BeforeDealingDamage,  // Trước khi đánh (Check BruteForce, Berserker)
    AfterDealingDamage,   // Sau khi đánh xong (Check LifeSteal - nếu có)
    BeforeTakingDamage,   // Trước khi bị trúng đòn (Check Stone Skin)
    AfterTakingDamage,    // Sau khi bị trúng đòn (Check Thorn)
    OnTurnEnd,            // Kết thúc lượt
    Special               // Các trường hợp hiếm gặp khác
}