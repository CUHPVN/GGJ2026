using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;
    public SlotMachine slotMachine;
    private void OnEnable()
    {
        StateController.Instance.OnEnterStateRoll += Close;
        slotMachine.OnRollDone += Open;
    }
    private void OnDisable()
    {
        if(StateController.Instance!=null)
        StateController.Instance.OnEnterStateRoll -= Close;
        slotMachine.OnRollDone -= Open;
    }
    private void Open()
    {
        animator.Play("Open");
    }
    private void Close()
    {
        animator.Play("Close");
    }
}
