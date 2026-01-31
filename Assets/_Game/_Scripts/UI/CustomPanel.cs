using UnityEngine;
using System.Collections;

public class CustomPanel : MonoBehaviour
{
    // ĐẢM BẢO PHẢI KÉO RECTTRANSFORM VÀO ĐÂY TRONG INSPECTOR
    public RectTransform panel;

    public Vector2 hiddenPos = new Vector2(0, -1200);
    public Vector2 visiblePos = Vector2.zero;
    public float speed = 12f;

    public void PopUp()
    {
        // Phải bật lên trước thì Coroutine mới chạy được
        gameObject.SetActive(true);

        // Đưa về vị trí ẩn ngay lập tức để bắt đầu trượt lên
        if (panel != null) panel.anchoredPosition = hiddenPos;

        StopAllCoroutines();
        StartCoroutine(Slide(visiblePos, null));
    }

    public void PopDown()
    {
        StopAllCoroutines();
        // Trượt xuống xong rồi mới tắt SetActive bằng Callback
        StartCoroutine(Slide(hiddenPos, () => gameObject.SetActive(false)));
    }

    IEnumerator Slide(Vector2 target, System.Action onComplete)
    {
        if (panel == null)
        {
            Debug.LogError("Bạn chưa kéo Panel vào script CustomPanel trên " + gameObject.name);
            yield break;
        }

        // Dùng SmoothStep hoặc Lerp để di chuyển
        while (Vector2.Distance(panel.anchoredPosition, target) > 1f)
        {
            panel.anchoredPosition = Vector2.Lerp(panel.anchoredPosition, target, Time.deltaTime * speed);
            yield return null;
        }

        panel.anchoredPosition = target;
        onComplete?.Invoke(); // Chạy lệnh tắt SetActive nếu có
    }
}