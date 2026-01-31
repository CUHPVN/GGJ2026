using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetSystem : MonoBehaviour
{
    [SerializeField] private int playerHealth = 10;
    [SerializeField] private int enemyHealth = 10;
    [SerializeField] private int totalPlayerBet = 0;
    [SerializeField] private int totalEnemyBet = 0;
    [SerializeField] private Button surrender;
    [SerializeField] private Button allIn;
    [SerializeField] private Button comfirmBet;
    [SerializeField] private Button incBet;
    [SerializeField] private Button decBet;
    [SerializeField] private TMP_Text totalPlayerBetText;
    [SerializeField] private TMP_Text totalEnemyBetText;
    [SerializeField] private TMP_Text betCountText;
    [SerializeField] private TMP_Text playerHealthText;
    [SerializeField] private TMP_Text enemyHealthText;

    [SerializeField] private MaskMoving maskMoving;




    private EntityTurn turn = EntityTurn.Enemy;
    private int betCount = 0;
    private int turnCount = 0;

    private Coroutine enemyThink;
    private Coroutine outOfHealth;

    private void OnEnable()
    {
        StateController.Instance.OnEnterStateBet += OnStartBetState;
        surrender.onClick.AddListener(() => Surrender());
        allIn.onClick.AddListener(() => AllIn());
        comfirmBet.onClick.AddListener(() => ConfirmBet());
        incBet.onClick.AddListener(() => IncBet());
        decBet.onClick.AddListener(() => DecBet());
    }
    private void OnDisable()
    {
        if (StateController.Instance != null)
        {
            StateController.Instance.OnEnterStateBet -= OnStartBetState;

        }
    }
    private void Start()
    {
        LoadLevel(10, 10);
    }
    public void LoadLevel(int playerHp, int enemyHp)
    {
        this.playerHealth = playerHp;
        OnPlayerHealthChange();
        this.enemyHealth = enemyHp;
        OnEnemyHealthChange();
        totalPlayerBet = 0;
        OnTotalPlayerBetChange();
        totalEnemyBet = 0;
        OnTotalEnemyBetChange();
    }
    private void OnStartBetState()
    {
        OnReset();
        StartEnemyThink();
    }
    private void StartEnemyThink()
    {
        if (turnCount >= 6)
        {
            turn = EntityTurn.Stop;
            Result();
            return;
        }
        if (turn == EntityTurn.Enemy)
        {
            if (enemyThink == null)
            {

                enemyThink = StartCoroutine(EnemyThink());
            }
        }
    }
    private IEnumerator EnemyThink()
    {
        yield return new WaitForSeconds(2f);

        enemyThink = null;
        EnemyTurn();
    }
    private void EnemyTurn()
    {

        int minHP = totalPlayerBet - totalEnemyBet;
        if(playerHealth>=1)
        {
            int roll = Random.Range(0, 100);
            if (roll < 80)
            {
                EnemyBet(Mathf.Max(minHP, 1));
            }
            else
            {
                EnemyBet(Mathf.Max(minHP, 1) + Random.Range(0,enemyHealth));
            }
        }else
        if (minHP >= enemyHealth)
        {
            EnemyBet(enemyHealth);
        }
        else
        {
            turn = EntityTurn.Stop;
            BetOutOfHealth();
        }
    }
    public void OnChangeEnemy()
    {

    }
    public void OnChangeQuestion()
    {
        totalEnemyBet = 0;
        OnTotalEnemyBetChange();
        totalPlayerBet = 0;
        OnTotalPlayerBetChange();
    }
    public void OnPlayerWin()
    {

    }
    void OnEnemyHealthChange()
    {
        enemyHealthText.text = enemyHealth.ToString();
    }
    void OnPlayerHealthChange()
    {
        playerHealthText.text = playerHealth.ToString();

    }
    void OnTotalEnemyBetChange()
    {
        totalEnemyBetText.text = totalEnemyBet.ToString();
    }
    void OnTotalPlayerBetChange()
    {
        totalPlayerBetText.text = totalPlayerBet.ToString();
    }
    void OnBetCountChange()
    {
        betCountText.text = betCount.ToString();
    }
    void Surrender()
    {
        if (turn != EntityTurn.Player) return;
        if (totalEnemyBet > 0)
        {
            enemyHealth += totalEnemyBet;
            OnEnemyHealthChange();
            totalEnemyBet = 0;
            OnTotalEnemyBetChange();
        }
        totalPlayerBet = 0;
        OnTotalPlayerBetChange();
        StateController.Instance.ChangeState(GameState.Roll);
    }
    void AllIn()
    {
        if (turn != EntityTurn.Player) return;
        betCount = playerHealth;
        OnBetCountChange();
    }
    void ConfirmBet()
    {
        if (turn != EntityTurn.Player) return;
        if (betCount > playerHealth) return;
        totalPlayerBet += betCount;
        OnTotalPlayerBetChange();
        playerHealth -= betCount;
        OnPlayerHealthChange();
        betCount = 0;
        OnBetCountChange();
        if (CheckEqual()) StartEnemyThink();
        if (turnCount >= 6)
        {
            turn = EntityTurn.Stop;
            Result();
            return;
        }
        if (totalPlayerBet < totalEnemyBet)
        {
            //Debug.Log("End");
            //turn = EntityTurn.Stop;
            BetOutOfHealth();
            return;
        }
        else
        {
            turn = EntityTurn.Enemy;
            StartEnemyThink();
        }
    }
    private void EnemyBet(int count)
    {
        totalEnemyBet += count;
        OnTotalEnemyBetChange();
        enemyHealth -= count;
        OnEnemyHealthChange();
        if (CheckEqual()&&playerHealth!=0&&enemyHealth!=0) StartEnemyThink();
        else
        if (turnCount >= 6)
        {
            turn = EntityTurn.Stop;
            Result();
            return;
        } else
        if (totalEnemyBet < totalPlayerBet)
        {
            //Debug.Log("End");
            //turn = EntityTurn.Stop;
            BetOutOfHealth();
            return;
        }
        else
        {
            if(playerHealth==0)
            {
                //turn = EntityTurn.Stop;
                BetOutOfHealth();
                return;
            }
            betCount = totalEnemyBet - totalPlayerBet;
            OnBetCountChange();
            turn = EntityTurn.Player;
        }
    }
    private bool CheckEqual()
    {
        if (totalEnemyBet == totalPlayerBet )
        {
            //Debug.Log("Equal");
            turnCount++;
            Reveal();
            return true;
        }
        return false;

    }
    private void BetOutOfHealth()
    {

        if (outOfHealth == null)
        {
            outOfHealth = StartCoroutine(OutOfHealth());
        }
    }
    private IEnumerator OutOfHealth()
    {
        while (turnCount<6) {
            yield return new WaitForSeconds(2f);
            //Debug.Log("OutOfHeath");
            turnCount++;
            Reveal();
            if(turnCount == 6)
            {
                turn = EntityTurn.Stop;
                Result();
            }
        }
        outOfHealth = null;
    }



    void Result()
    {
        EndBattleState endBattleState = QuestionManager.Instance.battleState;
        if(endBattleState == EndBattleState.Draw)
        {
            playerHealth += totalPlayerBet;
            OnPlayerHealthChange();
            totalPlayerBet = 0;
            OnTotalPlayerBetChange();
            enemyHealth += totalEnemyBet;
            OnEnemyHealthChange();
            totalEnemyBet = 0;
            OnTotalEnemyBetChange();
            //new 
            StateController.Instance.ChangeState(GameState.Roll);
        }
        else if(endBattleState == EndBattleState.Win)
        {
            //Addmoney
            playerHealth += totalPlayerBet;
            OnPlayerHealthChange();
            totalPlayerBet = 0;
            OnTotalPlayerBetChange();
            if (enemyHealth == 0)
            {
                //addItem
                //new 
            }
            else
            {
                totalEnemyBet = 0;
                OnTotalEnemyBetChange();
                //new 
            }
        }
        else if(endBattleState == EndBattleState.Lose) 
        {
            //Fade...
            //Menu
        }
    }
    void Reveal()
    {
        maskMoving.Move(turnCount);
    }
    void IncBet()
    {
        if (turn != EntityTurn.Player) return;

        if (betCount >= playerHealth) {
        }
        else
        {
            betCount++;
            OnBetCountChange();
        }
    }
    void DecBet()
    {
        if (turn != EntityTurn.Player) return;

        if (betCount<=totalEnemyBet-totalPlayerBet)
        {
            //pop
        }
        else
        {
            betCount--;
            OnBetCountChange();
        }
    }
    public void OnReset()
    {
        turn = EntityTurn.Enemy;
        betCount = 0;
        turnCount = 0;
    }

    public enum EntityTurn
    {
        Stop=0,
        Enemy=1,
        Player=2,
    }
}
   