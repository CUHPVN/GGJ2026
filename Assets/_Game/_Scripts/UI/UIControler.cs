using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIControler : MonoBehaviour
{
    [SerializeField] private SlotMachine slotMachine;
    public Appear[] appears;
    public Appear[] rollAppears;
    public Appear[] betAppears;
    public Appear[] firstBetAppears;
    public Appear[] winAppears;
    public Appear[] loseAppears;
    public Appear[] complAppears;
    public Appear[] rollDoneAppears;
    public Appear[] EnemyTurnAppears;
    public Appear[] PlayerTurnAppears;

    [Header("Dis")]
    public Appear[] rollDisappears;
    public Appear[] betDisappears;
    public Appear[] winDisappears;
    public Appear[] loseDisappears;
    public Appear[] complDisappears;
    public Appear[] rollDoneDisappears;
    public Appear[] EnemyTurnDisappears;
    public Appear[] PlayerTurnDisappears;




    private void OnEnable()
    {
        StateController.Instance.OnEnterStateRoll += RollState;
        StateController.Instance.OnEnterStateBet += BetState;
        BetSystem.Instance.BetCoinFist +=BetFistTime;
        BetSystem.Instance.TurnEvent += Turn;

        slotMachine.OnRollDone += RollDone;

    }
    private void OnDisable()
    {
        if (StateController.Instance != null)
        {

            StateController.Instance.OnEnterStateRoll -= RollState;
            StateController.Instance.OnEnterStateBet -= BetState;

        }
        if (BetSystem.Instance != null)
        {
            BetSystem.Instance.BetCoinFist -= BetFistTime;
            BetSystem.Instance.TurnEvent -= Turn;

        }
        slotMachine.OnRollDone -= RollDone;

    }
    private void Update()
    {
        //Keyboard keyboard = Keyboard.current;
        //if (keyboard.hKey.wasPressedThisFrame)
        //{
        //    Hide();
        //}
        //if (keyboard.sKey.wasPressedThisFrame)
        //{
        //    Show();
        //}
    }
    [ContextMenu("GetALL")]
    public void GetAll()
    {
        appears = FindObjectsByType<Appear>(FindObjectsSortMode.None);
    }
    public void Show()
    {
        foreach(Appear appear in appears)
        {
            appear.Show();
        }
    }
    public void Hide()
    {
        foreach (Appear appear in appears)
        {
            appear.Hide();
        }
    }
    public void BetFistTime()
    {
        foreach (Appear appear in firstBetAppears)
        {
            appear.Show();
        }
    }
    public void BetState()
    {
        foreach (Appear appear in betAppears)
        {
            appear.Show();
        }
        foreach (Appear appear in betDisappears)
        {
            appear.Hide();
        }
    }
    public void RollState()
    {
        foreach (Appear appear in rollAppears)
        {
            appear.Show();
        }
        foreach (Appear appear in rollDisappears)
        {
            appear.Hide();
        }
    }
    public void RollDone()
    {
        foreach (Appear appear in rollDoneAppears)
        {
            appear.Show();
        }
        foreach (Appear appear in rollDoneDisappears)
        {
            appear.Hide();
        }
    }
    public void Turn(EntityTurn entityTurn)
    {
        if (entityTurn == EntityTurn.Enemy)
        {
            EnemyTurn();
        }

        if (entityTurn == EntityTurn.Player)
        {
            PlayerTurn();
        }

        if (entityTurn == EntityTurn.Stop)
        {
            StopTurn();
        }
    }
    public void EnemyTurn()
    {
        foreach (Appear appear in EnemyTurnAppears)
        {
            appear.Show();
        }
        foreach (Appear appear in EnemyTurnDisappears)
        {
            appear.Hide();
        }
    }
    public void PlayerTurn()
    {
        foreach (Appear appear in PlayerTurnAppears)
        {
            appear.Show();
        }
        foreach (Appear appear in PlayerTurnDisappears)
        {
            appear.Hide();
        }
    }
    public void StopTurn()
    {
        
        foreach (Appear appear in PlayerTurnDisappears)
        {
            appear.Hide();
        }
        foreach (Appear appear in EnemyTurnDisappears)
        {
            appear.Hide();
        }
    }
}
