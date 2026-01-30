using UnityEngine;

public class TurnManager
{
    public void ExecuteTurn(EntityView attacker, EntityView defender)
    {
        // --- BƯỚC 1: PHASE ĐẦU LƯỢT (Hồi máu/Buff theo thời gian) ---
        CombatContext turnStartContext = new CombatContext { Defender = attacker };
        attacker.TriggerHooks(CombatEventType.OnTurnStart, ref turnStartContext);

        // --- BƯỚC 2: TÍNH TOÁN SỐ LẦN ĐÁNH (DEX Mechanic) ---
        // Công thức: 1 + floor(DEX / 50)
        int hitCounts = attacker.Stats.HitCounts;

        Debug.Log($"{attacker.entityName} bắt đầu chuỗi tấn công với {hitCounts} lần đánh!");

        // --- BƯỚC 3: VÒNG LẶP TẤN CÔNG (Multi-hit) ---
        for (int i = 0; i < hitCounts; i++)
        {
            if (defender.CurrentHP <= 0) break; // Dừng nếu đối thủ đã chết

            ExecuteSingleHit(attacker, defender);
        }
    }

    private void ExecuteSingleHit(EntityView attacker, EntityView defender)
    {
        // 1. Khởi tạo Context cho từng phát đánh
        CombatContext context = new CombatContext
        {
            Attacker = attacker,
            Defender = defender,
            DamageValue = attacker.Stats.PAtk, // Sát thương vật lý mặc định
            IsCrit = Random.Range(0f, 100f) < attacker.Stats.CritChance // Check tỉ lệ chí mạng
        };

        // 2. Xử lý nhân sát thương chí mạng
        if (context.IsCrit)
        {
            context.DamageValue *= 2f; // Hoặc hệ số bạn muốn
        }

        // 3. Hook: Trước khi gây dame (BruteForce, Berserker tính toán tại đây)
        attacker.TriggerHooks(CombatEventType.BeforeDealingDamage, ref context);

        // 4. Hook: Trước khi nhận dame (Stone Skin giảm dame tại đây)
        defender.TriggerHooks(CombatEventType.BeforeTakingDamage, ref context);

        // 5. Trừ giáp vật lý (P.Def từ STR)
        float finalDamage = Mathf.Max(1, context.DamageValue - defender.Stats.PDef);

        // 6. Thực hiện trừ máu và Clamp không cho dưới 0
        defender.CurrentHP = Mathf.Max(0, defender.CurrentHP - finalDamage);

        Debug.Log($"{attacker.entityName} đánh trúng {defender.entityName} gây {finalDamage} sát thương! (Crit: {context.IsCrit})");

        // 7. Hook: Sau khi gây dame
        attacker.TriggerHooks(CombatEventType.AfterDealingDamage, ref context);

        // 8. Hook: Sau khi nhận dame (Thorn phản dame tại đây)
        defender.TriggerHooks(CombatEventType.AfterTakingDamage, ref context);
    }
}