using System.Dynamic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Slot : MonoBehaviour
{
    [SerializeField] private SlotMachine slotMachine;
    [SerializeField] private int symbolCount;
    [SerializeField] private float speed=1f;
    [SerializeField] private int currentSymbol=0;
    [SerializeField] private int[] symbolArray;
    [SerializeField] private Icon[] icons;
    [SerializeField] private bool stop;
    private void Start()
    {
        StartRoll();
    }
    public void StartRoll()
    {
        stop = false;
        symbolArray = new int[symbolCount];
        for (int i = 0; i < symbolCount; i++)
        {
            symbolArray[i] = Random.Range(0, symbolCount); //Can Change
        }
        currentSymbol = 1;
        for (int i = 0; i < 3; i++)
        {
            icons[i].StartRoll();
            Vector2 target = Vector2.zero;
            target = (new Vector2(icons[i].transform.position.x, transform.position.y + i - 1f));
            icons[i].transform.position = target;
            icons[i].SetSlotMachine(slotMachine);
            icons[i].SetSymbolData(symbolArray[GetIndexInArray(currentSymbol - 1)]);
            icons[i].SetCurrentIndex(i);
        }
    }
    private void Update()
    {
        Roll();  
    }
    public void Roll()
    {
        if (stop) return;
        for (int i = 0; i < 3; i++)
        {
            icons[i].transform.position = new Vector2(icons[i].transform.position.x, icons[i].transform.position.y - Time.deltaTime * speed);
            if (icons[i].transform.position.y <= transform.position.y -1.5f)
            {
                icons[i].transform.position = new Vector2(icons[i].transform.position.x, icons[i].transform.position.y + 3f);
                currentSymbol++;
                currentSymbol %= symbolCount;
                for (int j = 0; j < 3; j++) icons[j].DecIndex();
                icons[i].SetSymbolData(symbolArray[GetIndexInArray(currentSymbol + 1)]);
            }
        }
    }
    public void SetStop(bool value)
    {
        stop = value;
        if (stop)
        {
            Debug.Log(slotMachine.GetNameSprite(symbolArray[GetIndexInArray(currentSymbol)]));//

            for(int i = 0; i < 3; i++)
            {
                int pos = icons[i].GetCurrentIndex();
                Vector2 target = Vector2.zero;
                if (pos == 2)
                {
                    target = (new Vector2(icons[i].transform.position.x,transform.position.y+ 1f));
                }
                else if(pos == 0)
                {
                    target = (new Vector2(icons[i].transform.position.x,transform.position.y-1f));
                }
                else if(pos == 1)
                {
                    target = (new Vector2(icons[i].transform.position.x, transform.position.y));
                }
                icons[i].SetTargetPosition(target);
            }
        }
    }
    public int GetResultSymbol()
    {
        if (!stop) return -1;
        return symbolArray[currentSymbol];
    }
    public void SetSlotMachine(SlotMachine value)
    {
        slotMachine = value;
    }
    public void SetSpeed(float value)
    {
        speed = value;
    }
    public void SetSymbolCount(int value)
    {
        symbolCount = value;
    }
    private int GetIndexInArray(int i)
    {
        if (i >= symbolCount)
        {
            return 0;
        }
        if (i < 0)
        {
            return symbolCount-1;
        }
        else
        {
            return i;
        }
    }
}
