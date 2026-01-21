using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    private TurnController turnController;
    [SerializeField] private List<int> heros;
    [SerializeField] private List<int> enemies;


    void Awake()
    {
        turnController = TurnController.Instance;
    }

    void Update()
    {
        if (turnController.GetCombatState() == PhaseState.Combat)
        {

        }
    }
}
