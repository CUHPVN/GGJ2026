using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class MaskMoving : MonoBehaviour
{
    private Coroutine coroutine;
    public AnimationCurve curve;
    [SerializeField] float slotSize = 1.125f;
    [SerializeField] int maxTurn = 6;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Vector2 startPos;
    public void OnEnable()
    {
        StateController.Instance.OnEnterStateRoll += ResetMask;

    }
    public void OnDisable()
    {
        if (StateController.Instance != null)
        {
            StateController.Instance.OnEnterStateRoll -= ResetMask;
        }
    }
    public void ResetMask()
    {
        transform.position = startPos;
        spriteRenderer.color = Color.white;
    }
    public void Move(int value)
    {
        if (value > maxTurn) return;
        Vector2 target =(new Vector2((value % 2) * slotSize, -(1 - value % 2) * 1));
        if (coroutine == null)
        {
            coroutine = StartCoroutine(MoveToPos((Vector2) transform.position + target));

        }
        if(value == 6)
        {
            StartCoroutine(Blur());
        }
    }
    public IEnumerator MoveToPos(Vector2 target)
    {
        float time = 0;
        float duration = 0.2f;
        Vector2 startPos = transform.position;
        while (time < duration)
        {
            time += Time.deltaTime;
            transform.position = Vector2.Lerp(startPos, target, curve.Evaluate(time / duration));   
            yield return null;
        }
        transform.position = target;
        coroutine = null;
    }
    public IEnumerator Blur()
    {
        float time = 0;
        float duration = 0.2f;
        Color startPos = spriteRenderer.color;
        Color targetCol = new Color(0, 0, 0, 0);
        while (time < duration)
        {
            time += Time.deltaTime;
            spriteRenderer.color = Vector4.Lerp(startPos, targetCol, curve.Evaluate(time / duration));
            yield return null;
        }
        spriteRenderer.color = targetCol;
        coroutine = null;
    }
}
