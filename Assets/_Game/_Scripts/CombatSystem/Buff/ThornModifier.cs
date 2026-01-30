public class ThornModifier : BaseModifier
{
    public override CombatEventType TriggerType => CombatEventType.AfterTakingDamage;

    public ThornModifier(int level, int duration) : base(level, duration)
    {
        ID = ModifierID.Thorn;
    }

    public override void OnEventTriggered(ref CombatContext context)
    {
        // Chỉ phản sát thương nếu là đòn đánh vật lý (không phải phép)
        // Giả sử context có biến IsMagicalDamage
        if (context.IsMagical) return;

        float reflectPercent = 0.10f + (Level * 0.05f); // Lvl 1: 15%
        float damageToReflect = context.DamageValue * reflectPercent;

        // Trừ máu kẻ tấn công
        context.Attacker.CurrentHP -= damageToReflect;

        UnityEngine.Debug.Log($"[Thorn Lvl {Level}] Phản {damageToReflect} sát thương vật lý!");
    }
}