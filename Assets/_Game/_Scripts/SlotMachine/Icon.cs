using System;
using System.Collections;
using Tech.Events;
using UnityEngine;
using UnityEngine.Events;

public class Icon : MonoBehaviour
{
    [SerializeField] private SlotMachine slotMachine;
    [SerializeField] private SpriteRenderer symbolSprite;
    [SerializeField] private int symbolData;
    [SerializeField] private Vector2 TargetPosition;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private int currentIndex=0;
    public void SetSymbolData(int value)
    {
        symbolData = value;
        symbolSprite.sprite = slotMachine.GetSymbolSpriteRule(symbolData);
    }
    public int GetSymbolData()
    {
        return symbolData;
    }
    public void SetSlotMachine(SlotMachine value)
    {
        slotMachine = value;
    }
    public void SetTargetPosition(Vector2 targetPosition)
    {
        TargetPosition = targetPosition;
        StartCoroutine(Move());
    }
    public void SetCurrentIndex (int index)
    {
        currentIndex = index;
    }
    public int GetCurrentIndex() => currentIndex;
    public void DecIndex()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = 2;
    }
    IEnumerator Move()
    {
        float time = 0;
        float duration = 0.15f;
        Vector2 start = transform.position;
        while (time < duration)
        {
            time += Time.deltaTime;
            transform.position= Vector3.Lerp(start, TargetPosition, curve.Evaluate(time / (duration)));
            yield return null;
        }
        transform.position=TargetPosition;
    }
    public void StartRoll()
    {
        StopAllCoroutines();
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
   
}
