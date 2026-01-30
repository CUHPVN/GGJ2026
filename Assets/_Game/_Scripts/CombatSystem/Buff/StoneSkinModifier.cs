using System;

public class StoneSkinModifier : BaseModifier
{
    public override CombatEventType TriggerType => CombatEventType.BeforeTakingDamage;

    public StoneSkinModifier(int level, int duration) : base(level, duration)
    {
        ID = ModifierID.StoneSkin;
    }

    public override void OnEventTriggered(ref CombatContext context)
    {
        float reduction = Level * 10f;

        context.DamageValue = Math.Max(1, context.DamageValue - reduction);

        UnityEngine.Debug.Log($"[Stone Skin Lvl {Level}] Giảm {reduction} sát thương nhận vào.");
    }
}