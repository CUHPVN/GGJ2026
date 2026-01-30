using System.Collections;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Entities")]
    public EntityView player;
    public EnemyEntity enemy;

    [Header("Systems")]
    public SlotMachine slotMachine;
    private TurnManager turnManager = new TurnManager();

    private void Start()
    {
        // Kh?i t?o ch? s? ban ??u cho level
        player.Initialize(10, 5, 60); // 60 DEX = 2 hits
        enemy.Initialize(8, 8, 20);

        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        Debug.Log("Waiting for Slot Machine...");
        // Gi? s? slotMachine có m?t bi?n IsDone
        yield return new WaitUntil(() => slotMachine.GetIsDone());

        // 2. Giai ?o?n Coin Flip
        bool playerGoFirst = UnityEngine.Random.value > 0.5f;
        Debug.Log(playerGoFirst ? "Player!" : "Enemy!");

        // 3. Vòng l?p chi?n ??u cho ??n khi có ng??i ch?t
        while (player._currentHP > 0 && enemy._currentHP > 0)
        {
            if (playerGoFirst)
            {
                yield return StartCoroutine(PlayerTurnRoutine());
                if (enemy._currentHP <= 0) break;
                yield return StartCoroutine(EnemyTurnRoutine());
            }
            else
            {
                yield return StartCoroutine(EnemyTurnRoutine());
                if (player._currentHP <= 0) break;
                yield return StartCoroutine(PlayerTurnRoutine());
            }
            yield return new WaitForSeconds(1f); // Ngh? gi?a các turn
        }

        //Debug.Log("Tr?n ??u k?t thúc!");
    }

    IEnumerator PlayerTurnRoutine()
    {
        Debug.Log("--- L??t c?a Player ---");
        // G?i logic t? TurnManager ?ã refactor
        turnManager.ExecuteTurn(player, enemy);
        yield return new WaitForSeconds(0.5f); // Th?i gian cho animation
    }

    IEnumerator EnemyTurnRoutine()
    {
        Debug.Log("--- L??t c?a Enemy ---");
        enemy.DecideAction(out bool isMagical);
        turnManager.ExecuteTurn(enemy, player);
        yield return new WaitForSeconds(0.5f);
    }
}