using UnityEngine;

public abstract class BaseModifier : ICombatModifier
{
    public ModifierID ID { get; protected set; }
    public string Name { get; protected set; }
    public int Level { get; protected set; } // 1, 2, hoặc 3
    public int Duration { get; set; } 
    public abstract CombatEventType TriggerType { get; }
    public virtual int Priority => 50;

    protected BaseModifier(int level, int duration)
    {
        ID = ModifierID.None;
        Level = Mathf.Clamp(level, 1, 3);
        Duration = duration;
    }

    public abstract void OnEventTriggered(ref CombatContext context);

    // Hàm để tính toán giá trị dựa trên Level
    protected float ScaleValue(float baseValue, float growthFactor)
    {
        // Ví dụ: Level 1 = 100%, Level 2 = 150%, Level 3 = 200%
        return baseValue + (baseValue * growthFactor * (Level - 1));
    }
}