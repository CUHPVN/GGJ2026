using System.Collections;
using UnityEngine;

public class Appear : MonoBehaviour
{
    [SerializeField] private Transform targetA;
    [SerializeField] private Vector2 target;
    public AnimationCurve curve;
    private Coroutine coroutine;
    private bool isShow = false;

    public void Awake()
    {
        if (!isShow)
        {
            targetA.gameObject.SetActive(false);
            targetA.transform.position = (Vector2)targetA.transform.position - this.target;
        }
    }
    public void Show()
    {
        if(coroutine == null&&!isShow) 
        coroutine = StartCoroutine(EShow());
    }
    public void Hide()
    {
        if (coroutine == null&&!isShow)
            coroutine = StartCoroutine(EHide());
    }
    public IEnumerator EShow()
    {
        targetA.transform.gameObject.SetActive(true);
        float time=0;
        float duration = 0.2f;
        Vector2 startPos = targetA.transform.position;
        Vector2 target = (Vector2)transform.position;
        while (time < duration)
        {
            time += Time.deltaTime;
            targetA.transform.position = Vector2.Lerp(startPos, target, curve.Evaluate(time / duration));
            yield return null;
        }
        targetA.transform.position = target;
        coroutine = null;
    }
    public IEnumerator EHide()
    {
        float time = 0;
        float duration = 0.1f;
        Vector2 startPos = targetA.transform.position;
        Vector2 target = (Vector2)targetA.transform.position - this.target;
        while (time < duration)
        {
            time += Time.deltaTime;
            targetA.transform.position = Vector2.Lerp(startPos, target, curve.Evaluate(time / duration));
            yield return null;
        }
        targetA.transform.position = target;
        targetA.transform.gameObject.SetActive(false);
        coroutine = null;
    }
}
