using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BetSystem : Singleton<BetSystem>
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
    private Coroutine totalPlayerBetCoroutine;
    private Coroutine totalEnemyBetCoroutine;

    [SerializeField] private MaskMoving maskMoving;

    public event Action BetCoinFist;
    public event Action<EntityTurn> TurnEvent;
    public bool IsBet=false;

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
    }
    public void LoadLevel(int enemyHp)
    {
        this.enemyHealth = enemyHp;
        OnEnemyHealthChange();
        totalPlayerBet = 0;
        OnTotalPlayerBetChange();
        totalEnemyBet = 0;
        OnTotalEnemyBetChange();
    }

    
    public void LoadLevelFistTime(int playerHp, int enemyHp)
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
            TurnEvent?.Invoke(turn);
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
        if(minHP<=enemyHealth&&enemyHealth>=1)
        {
            int roll = UnityEngine.Random.Range(0, 100);
            if (roll < 70)
            {
                int roll2 = UnityEngine.Random.Range(0, 100);
                if (roll2 < 50)
                {
                    EnemyBet(Mathf.Max(minHP, 1));
                }
                else
                {
                    int minCl = Mathf.Max(playerHealth, enemyHealth)/3;
                    EnemyBet(UnityEngine.Random.Range(Mathf.Max(minHP, 1), minCl));
                }
            }
            else
            {
                EnemyBet(Mathf.Max(minHP, 1) + UnityEngine.Random.Range(0,enemyHealth));
            }
        }else
        if (minHP > enemyHealth)
        {
            EnemyBet(enemyHealth);
        }
        else
        {
            turn = EntityTurn.Stop;
            TurnEvent?.Invoke(turn);

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

        if (totalEnemyBetCoroutine != null) StopCoroutine(totalEnemyBetCoroutine);

        // Hiệu ứng cho tiền cược của quái
        totalEnemyBetCoroutine = StartCoroutine(UIManager.PunchScale(totalEnemyBetText.rectTransform, 1.3f, 0.25f));
    }
    void OnTotalPlayerBetChange()
    {
        totalPlayerBetText.text = totalPlayerBet.ToString();

        if (totalPlayerBetCoroutine != null) StopCoroutine(totalPlayerBetCoroutine);

        // Hiệu ứng cho tiền cược của quái
        totalPlayerBetCoroutine = StartCoroutine(UIManager.PunchScale(totalPlayerBetText.rectTransform, 1.3f, 0.25f));
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
            totalEnemyBet = 0;
            OnTotalEnemyBetChange();
        }
        enemyHealth += totalPlayerBet;
        enemyHealth += totalPlayerBet / 2;
        totalPlayerBet = 0;
        OnEnemyHealthChange();
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
        if (!IsBet)
        {
            IsBet = true;
            BetCoinFist?.Invoke();
        }
        if (CheckEqual()) StartEnemyThink();
        if (turnCount >= 6)
        {
            turn = EntityTurn.Stop;
            TurnEvent?.Invoke(turn);

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
            TurnEvent?.Invoke(turn);

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
            TurnEvent?.Invoke(turn);

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
            betCount = Mathf.Min(betCount, playerHealth);
            OnBetCountChange();
            turn = EntityTurn.Player;
            TurnEvent?.Invoke(turn);

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
                TurnEvent?.Invoke(turn);

                Result();
            }
        }
        outOfHealth = null;
    }



    void Result()
    {
        EndBattleState endBattleState = QuestionManager.Instance.battleState;
        
        if (endBattleState == EndBattleState.Draw)
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
            //wait..
            StartCoroutine(EndDelay());
        }
        else if(endBattleState == EndBattleState.Win)
        {
            //Addmoney
            PlayerPrefs.SetInt("PlayerCoin",PlayerPrefs.GetInt("PlayerCoin") + totalEnemyBet);
            PlayerPrefs.Save();


            playerHealth += totalPlayerBet;
            playerHealth += totalEnemyBet/2;
            OnPlayerHealthChange();
            totalPlayerBet = 0;
            OnTotalPlayerBetChange();
            if (enemyHealth <= 0)
            {
                QuestionManager.Instance.playerHave[QuestionManager.Instance.question.type] = true;

                StateController.Instance.OnEnterStateBet += LevelUp;
                //new 
            }
            else
            {
                totalEnemyBet = 0;
                OnTotalEnemyBetChange();
                //new 
            }
            StartCoroutine(EndDelay());
        }
        else if(endBattleState == EndBattleState.Lose) 
        {
            enemyHealth += totalPlayerBet;
            enemyHealth += totalPlayerBet / 2;
            if (totalPlayerBet > totalEnemyBet)
            {
                playerHealth += totalPlayerBet - totalEnemyBet;
                OnPlayerHealthChange();
            }
            if (totalPlayerBet < totalEnemyBet)
            {
                enemyHealth += totalEnemyBet - totalPlayerBet;
                OnTotalEnemyBetChange();
            }
            OnTotalPlayerBetChange();


            if(playerHealth<=0)
            StartCoroutine(MenuDelay());
            else
                StartCoroutine(EndDelay());
        }
    }
    private void LevelUp()
    {
        LevelManager.Instance.LevelUp();
        StateController.Instance.OnEnterStateBet -= LevelUp;
    }
    private IEnumerator EndDelay()
    {
        yield return new WaitForSeconds(3f);
        StateController.Instance.ChangeState(GameState.Roll);
    }

    private IEnumerator MenuDelay()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Main Menu");
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
        TurnEvent?.Invoke(turn);

        betCount = 0;
        OnBetCountChange();
        turnCount = 0;
        totalPlayerBet = 0;
        OnTotalPlayerBetChange();
        totalEnemyBet = 0;
        OnTotalEnemyBetChange();
        IsBet=false;
    }

}
    public enum EntityTurn
    {
        Stop=0,
        Enemy=1,
        Player=2,
    }
   