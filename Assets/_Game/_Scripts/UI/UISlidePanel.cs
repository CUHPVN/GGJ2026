using UnityEngine;
using System.Collections;
public class UISlidePanel : MonoBehaviour
{
    public RectTransform panel;
    public Vector2 hiddenPos = new Vector2(0, -1000); // Vị trí ngoài màn hình
    public Vector2 visiblePos = Vector2.zero;
    public float speed = 10f;

    public void TogglePanel(bool show)
    {
        StopAllCoroutines();
        StartCoroutine(Slide(show ? visiblePos : hiddenPos));
    }

    IEnumerator Slide(Vector2 target)
    {
        while (Vector2.Distance(panel.anchoredPosition, target) > 0.1f)
        {
            panel.anchoredPosition = Vector2.Lerp(panel.anchoredPosition, target, Time.deltaTime * speed);
            yield return null;
        }
        panel.anchoredPosition = target;
    }
}