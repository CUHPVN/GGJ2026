using TMPro;
using UnityEngine;

public class TextLog : MonoBehaviour
{
    [SerializeField] private SlotMachine slotMachine;
    [SerializeField] private TMP_Text text;
    private void OnEnable()
    {
    }
    public void ShowText(string text)
    {

        this.text.text = text;
    }
}
