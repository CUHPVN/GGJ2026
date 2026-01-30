using System;
using UnityEngine;

public class StateController : Singleton<StateController>
{
    public GameState CurrentState = GameState.None;
    public event Action OnEnterStateRoll;
    public event Action OnEnterStateBet;
    public event Action OnEnterStateWin;
    public event Action OnEnterStateLose;
    public event Action OnEnterStateComplete;


    void Start()
    {
        ChangeState(GameState.Roll);
    }

    void Update()
    {
        //// Handle logic that needs to run every frame based on the state
        //switch (CurrentState)
        //{
        //    case GameState.Roll:
        //        // Handle rolling animation or physics here
        //        break;
        //    case GameState.Bet:
        //        // Wait for player input
        //        break;
        //}
    }

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;

        // Logic for EXITING the old state
        Debug.Log($"Exiting: {CurrentState}");

        CurrentState = newState;

        // Logic for ENTERING the new state
        Debug.Log($"Entering: {CurrentState}");

        switch (CurrentState) { 
        
            case GameState.Bet:
                StartBet();
                break;
            case GameState.Complete:
                HandleComplete();
                break;
            case GameState.Roll:
                StartRolling();
                break;
            case GameState.Win:
                HandleWin();
                break;
            case GameState.Lose:
                HandleLose();
                break;
        }
    }

    private void StartRolling() {
        OnEnterStateRoll?.Invoke();
    }
    private void StartBet()
    {
        OnEnterStateRoll?.Invoke();
    }
    private void HandleComplete() { /* Show UI and FX */ }

    private void HandleWin() { /* Show UI and FX */ }
    private void HandleLose() { /* Reset Bet */ }
}

public enum GameState
{
    None = 0,
    Roll = 1,
    Bet = 2,
    Win = 3,
    Lose = 4,
    Complete = 5,
}