public interface ICombatModifier
{
    ModifierID ID { get; }            // Định danh (Thorn, StoneSkin...)
    CombatEventType TriggerType { get; } // Thời điểm kích hoạt
    int Priority { get; }             // Độ ưu tiên (Ví dụ: Giảm dame phẳng tính trước, % tính sau)

    void OnEventTriggered(ref CombatContext context);
}

// Chứa thông tin về một hành động gây damage
public struct CombatContext
{
    public EntityView Attacker; // Người gây dame
    public EntityView Defender; // Người nhận dame

    public float DamageValue; // Giá trị sát thương (có thể bị thay đổi bởi Buff/Giáp)
    public bool IsCrit;       // Xác định đòn đánh có chí mạng hay không
    public bool IsMagical;    // Phân biệt P.Atk (STR) và M.Atk (INT)

    // Thuộc tính bổ trợ cho UI hoặc Logic đặc biệt
    public ModifierID SourceID;
}