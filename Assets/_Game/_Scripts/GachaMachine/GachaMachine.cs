using UnityEngine;
using System.Collections.Generic;
public class GachaMachine : MonoBehaviour
{
    [SerializeField] List<Sprite> gachaPool;
    [SerializeField] SpriteRenderer gachaResult;
    public void StartGacha()
    {
        int idx = Random.Range(0,gachaPool.Count);
        gachaResult.sprite = gachaPool[idx];
    }
}
