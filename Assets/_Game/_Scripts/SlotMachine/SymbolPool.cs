using System.Collections.Generic;
using UnityEngine;

public class SymbolPool : Singleton<SymbolPool>
{
    private int[] Pool=new int[150];
    public int curInx=0;
    public void RandomPool()
    {
        for (int i = 0; i < 100; i++)
        {
            Pool[i]=((i % 5));
        }
        for(int i=0; i < 50; i++)
        {
            Pool[i+100]=(Random.Range(0, 4));
        }
        curInx = 0;
        ListExtensions.Shuffle(Pool);
    }
    public int GetValueFormPool()
    {
        if (curInx >= 150) return 0;
        return Pool[curInx++];
    }

}
