using System;
using UnityEngine;

public class InteractManager : Singleton<InteractManager>
{
    public event Action OnRollAction;
    public event Action OnApplyRollAction;
    public event Action OnBetAction;
    public event Action OnGiveUpAction;
    public event Action OnAllInAction;
    public event Action OnUpAction;
    public event Action OnDownAction;

    void Start()
    {
        
    }

    void Update()
    {
    }
    public void RollButton()
    {
        OnRollAction?.Invoke();
    }
    public void ApplyRollButton()
    {
        OnApplyRollAction?.Invoke();
    }
}
