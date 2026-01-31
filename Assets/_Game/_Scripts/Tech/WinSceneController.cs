using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class WinSceneController : MonoBehaviour
{
    [Header("UI References")]
    public Image fadePanel;      // Kéo FadePanel vào đây
    public TextMeshProUGUI winText; // Kéo WinText vào đây

    [Header("Settings")]
    public float fadeSpeed = 2.5f;    // Tốc độ tối dần
    public float textDelay = 2f;  // Thời gian chờ trước khi hiện chữ

    void Start()
    {
        // Đảm bảo lúc bắt đầu mọi thứ đều trong suốt
        fadePanel.color = new Color(0, 0, 0, 0);
        winText.color = new Color(winText.color.r, winText.color.g, winText.color.b, 0);

        // Chạy hiệu ứng
        StartCoroutine(WinSequence());
    }

    IEnumerator WinSequence()
    {
        // 1. Màn hình tối dần lại
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadePanel.color = new Color(0, 0, 0, 1); // Đảm bảo đen hoàn toàn

        // 2. Chờ một chút trước khi hiện chữ
        yield return new WaitForSeconds(textDelay);

        // 3. Hiện chữ "You Win" đột ngột kèm hiệu ứng phóng to (Punch Scale)
        winText.color = new Color(winText.color.r, winText.color.g, winText.color.b, 1);

        // Gọi lại hiệu ứng phóng to mà bạn đã học ở phần trước
        StartCoroutine(PunchScaleText(winText.rectTransform, 1.5f, 0.4f));

        // 4. Phát âm thanh thắng cuộc (nếu có AudioManager)
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.Play(AudioManager.SoundType.BGM_Main); // Hoặc âm thanh Win riêng
        }
    }

    IEnumerator PunchScaleText(RectTransform rect, float punchFactor, float duration)
    {
        Vector3 originalScale = Vector3.one;
        Vector3 targetScale = originalScale * punchFactor;

        // Phóng to nhanh
        float elapsed = 0f;
        while (elapsed < duration * 0.3f)
        {
            elapsed += Time.deltaTime;
            rect.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / (duration * 0.3f));
            yield return null;
        }

        // Thu nhỏ mượt
        elapsed = 0f;
        while (elapsed < duration * 0.7f)
        {
            elapsed += Time.deltaTime;
            rect.localScale = Vector3.Lerp(targetScale, originalScale, elapsed / (duration * 0.7f));
            yield return null;
        }
        rect.localScale = originalScale;
    }
}