using UnityEngine;

public class GachaButton : MonoBehaviour
{
    [SerializeField] GachaMachine gachaMachine;
    public void OnClick()
    {
        gachaMachine.StartGacha();
    }
}
