using System;

public class RegenerationModifier : BaseModifier
{
    public override CombatEventType TriggerType => CombatEventType.OnTurnStart;

    public RegenerationModifier(int level, int duration) : base(level, duration)
    {
        ID = ModifierID.Regeneration;
    }

    public override void OnEventTriggered(ref CombatContext context)
    {
        // Ở Hook OnTurnStart, context.Defender chính là người đang được hồi máu
        float healAmount = context.Defender.Stats.MaxHP * (Level * 0.10f);

        context.Defender.CurrentHP = Math.Min(
            context.Defender.Stats.MaxHP,
            context.Defender.CurrentHP + healAmount
        );

        UnityEngine.Debug.Log($"[Regen Lvl {Level}] Hồi {healAmount} HP khi bắt đầu lượt.");
    }
}