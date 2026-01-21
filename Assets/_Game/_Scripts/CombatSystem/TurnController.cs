using UnityEngine;
using UnityEngine.InputSystem;

public class TurnController : Singleton<TurnController>
{
    [SerializeField] private PhaseState combatPhase = PhaseState.Prepare;
    void Start()
    {
        
    }

    void Update()
    {
        OnUpdate();
    }
    private void OnUpdate()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartBattle();
        }
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            StartBattle();
        }
    }
    public PhaseState GetCombatState() => combatPhase;
    public void StartBattle()
    {
        if (combatPhase!= PhaseState.Combat)
        {
            combatPhase = PhaseState.Combat;
        }
    }
    public void StartPrepare()
    {
        if (combatPhase != PhaseState.Prepare)
        {
            combatPhase = PhaseState.Prepare;
        }
    }

}

public enum PhaseState
{
    Prepare=0,
    Combat=1,
}
