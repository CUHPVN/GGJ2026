using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIControler : MonoBehaviour
{
    [SerializeField] private SlotMachine slotMachine;
    public Appear[] appears;
    public Appear[] rollAppears;
    public Appear[] betAppears;
    public Appear[] winAppears;
    public Appear[] loseAppears;
    public Appear[] complAppears;
    public Appear[] rollDoneAppears;

    [Header("Dis")]
    public Appear[] rollDisappears;
    public Appear[] betDisappears;
    public Appear[] winDisappears;
    public Appear[] loseDisappears;
    public Appear[] complDisappears;


    private void OnEnable()
    {
        StateController.Instance.OnEnterStateRoll += RollState;
        StateController.Instance.OnEnterStateBet += BetState;

        slotMachine.OnRollDone += RollDone;
    }
    private void OnDisable()
    {
        if (StateController.Instance != null)
        {

            StateController.Instance.OnEnterStateRoll -= RollState;
        }
    }
    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard.hKey.wasPressedThisFrame)
        {
            Hide();
        }
        if (keyboard.sKey.wasPressedThisFrame)
        {
            Show();
        }
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
    }
}
