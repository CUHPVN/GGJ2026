public enum ModifierID
{
    None = 0,

    // Nhóm Buff/Debuff chỉ số
    Thorn = 1,          // Phản sát thương vật lý
    StoneSkin = 2,      // Giảm sát thương cố định (Flat)
    Regeneration = 3,   // Hồi máu mỗi lượt

    // Nhóm Buff điều kiện/tỷ lệ
    BruteForce = 4,     // X2 Dame khi dưới 30% HP
    Berserker = 5,      // Mất máu tăng Dame

    // Nhóm Special (Dành cho Slot Machine)
    Reroll = 10,        // Quay lại
    LockSlot = 11       // Khóa ô
}