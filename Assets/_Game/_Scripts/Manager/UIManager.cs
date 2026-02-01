using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class UIManager : MonoBehaviour
{
    [SerializeField] CustomPanel shopPanel;
    [SerializeField] CustomPanel menuPanel;
    [SerializeField] CustomPanel settingPanel;
    public void OpenShop()
    {
        menuPanel.PopDown();
        shopPanel.PopUp();
    }
    public void CloseShop()
    {
        shopPanel.PopDown();
        menuPanel.PopUp();
    }

    public void OpenSetting()
    {
        menuPanel.PopDown();
        settingPanel.PopUp();
        
    }
    public void CloseSetting()
    {
        settingPanel.PopDown();
        menuPanel.PopUp();
    }
    public void Click()
    {
        AudioManager.Instance.Play(AudioManager.SoundType.Mouse_Click);
    }
    public void Roll()
    { AudioManager.Instance.Play(AudioManager.SoundType.Button_Click); }

    public void Play()
    {
        SceneManager.LoadScene("Main");
    }
    public static IEnumerator PunchScale(RectTransform rect, float punchFactor, float duration)
    {
        if (rect == null) yield break;

        Vector3 originalScale = Vector3.one;
        Vector3 targetScale = originalScale * punchFactor;

        float elapsed = 0f;
        float punchDuration = duration * 0.2f; 
        while (elapsed < punchDuration)
        {
            elapsed += Time.deltaTime;
            rect.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / punchDuration);
            yield return null;
        }

        elapsed = 0f;
        float returnDuration = duration * 0.8f; 
        while (elapsed < returnDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / returnDuration;
            rect.localScale = Vector3.Lerp(targetScale, originalScale, t);
            yield return null;
        }

        rect.localScale = originalScale;
    }

}
