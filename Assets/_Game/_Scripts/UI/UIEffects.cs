using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UIEffects : MonoBehaviour
{
    // 1. Shake Effect: Rung lắc UI (ví dụ khi không đủ tiền)
    public static IEnumerator Shake(RectTransform rect, float duration, float magnitude)
    {
        Vector3 orignalPos = rect.localPosition;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            rect.localPosition = new Vector3(orignalPos.x + x, orignalPos.y + y, orignalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rect.localPosition = orignalPos;
    }

    // 2. Number Ticker: Chạy số tiền mượt mà
    public static IEnumerator NumberTicker(TextMeshProUGUI text, int startVal, int endVal, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            int current = (int)Mathf.Lerp(startVal, endVal, elapsed / duration);
            text.text = "Coins: " + current.ToString();
            yield return null;
        }
        text.text = "Coins: " + endVal.ToString();
    }
}