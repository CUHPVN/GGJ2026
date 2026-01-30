using UnityEngine;
using System.Collections.Generic;

public abstract class EntityView : MonoBehaviour
{
    [Header("Entity Info")]
    public string entityName;

    // Logic chỉ số và danh sách Modifier
    public CharacterStats _stats;
    public float _currentHP;
    public List<ICombatModifier> _modifiers = new List<ICombatModifier>();

    // Truy cập dữ liệu
    public CharacterStats Stats => _stats;
    public float CurrentHP { get => _currentHP; set => _currentHP = value; }

    public virtual void Initialize(float str, float intel, float dex)
    {
        _stats = new CharacterStats(str, intel, dex); // Khởi tạo theo công thức quy đổi
        _currentHP = _stats.MaxHP; // 1 STR = 10 HP
    }

    // Cơ chế kích hoạt Hook (Modifier System)
    public void TriggerHooks(CombatEventType type, ref CombatContext context)
    {
        // Sắp xếp Priority để đảm bảo thứ tự tính toán (Ví dụ: Giảm dame phẳng trước, % sau)
        _modifiers.Sort((a, b) => a.Priority.CompareTo(b.Priority));

        foreach (var mod in _modifiers)
        {
            if (mod.TriggerType == type)
                mod.OnEventTriggered(ref context);
        }
    }

    public void AddModifier(ICombatModifier mod) => _modifiers.Add(mod);
}