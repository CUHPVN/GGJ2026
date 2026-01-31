using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SymbolPoolVisual : MonoBehaviour
{
    [SerializeField] private SymbolPool symbolPool;
    [SerializeField] private SlotMachine slotMachine;
    public TMP_Text[] counts = new TMP_Text[5];
    public Image[] images = new Image[5];

    private void OnEnable()
    {
        symbolPool.OnLoadPoolDone += LoadSymbol;
    }
    private void OnDisable()
    {
        symbolPool.OnLoadPoolDone -= LoadSymbol;
    }
    private void LoadSymbol()
    {
        int[] tmp = symbolPool.GetCount();
        for(int i = 0; i < 5; i++)
        {
            counts[i].text = ": " + tmp[i];
            images[i].sprite = slotMachine.GetSymbolSpriteRule(i);
        }
    }
}
