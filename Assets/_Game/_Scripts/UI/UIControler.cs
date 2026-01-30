using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIControler : MonoBehaviour
{
    public Appear[] appears;
    public Appear[] rollAppears;
    public Appear[] betAppears;
    public Appear[] winAppears;
    public Appear[] loseAppears;
    public Appear[] complAppears;

    [Header("Dis")]
    public Appear[] rollDisappears;
    public Appear[] betDisappears;
    public Appear[] winDisappears;
    public Appear[] loseDisappears;
    public Appear[] complDisappears;


    private void OnEnable()
    {
        StateController.Instance.OnEnterStateRoll += RollState;
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
}
