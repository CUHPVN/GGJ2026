using System;
using System.Collections.Generic;
using UnityEngine;

public class SymbolPool : Singleton<SymbolPool>
{
    public event Action OnLoadPoolDone;
    private int[] Pool=new int[150];
    private int[] Count = new int[5];
    public int curInx=0;
    public void RandomPool()
    {
        for (int i = 0; i < 100; i++)
        {
            Pool[i]=((i % 5));
        }
        Count[0] = 20;
        Count[1] = 20;
        Count[2] = 20;
        Count[3] = 20;
        Count[4] = 20;
        for(int i=0; i < 50; i++)
        {
            int value = (UnityEngine.Random.Range(0, 4));
            Pool[i + 100] = value;
            Count[value]++;
        }
        curInx = 0;
        ListExtensions.Shuffle(Pool);
        OnLoadPoolDone?.Invoke();
    }
    public int[] GetCount() { return Count; }
    public int GetValueFormPool()
    {
        if (curInx >= 150) return 0;
        return Pool[curInx++];
    }

}
