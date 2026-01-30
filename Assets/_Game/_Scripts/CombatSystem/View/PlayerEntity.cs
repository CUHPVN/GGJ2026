using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEntity : EntityView
{
    [Header("UI References")]
    [SerializeField] protected Slider hpSlider;
    private Coroutine hpCoroutine;
    public void OnEnable()
    {
        OnHealthChanged += UpdateVisualHealth;
    }
    public void OnDisable()
    {
        OnHealthChanged -= UpdateVisualHealth;
    }
    public void UpdateVisualHealth(float cur,float max)
    {
        if (hpSlider != null)
        {
            if (hpCoroutine != null) StopCoroutine(hpCoroutine);
            hpCoroutine = StartCoroutine(SmoothUpdateHP());
        }
    }

    private IEnumerator SmoothUpdateHP()
    {
        float targetValue = _currentHP/Stats.MaxHP;
        float startValue = hpSlider.value;
        float duration = 0.5f; 
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            hpSlider.value = Mathf.Lerp(startValue, targetValue, elapsed / duration);
            yield return null;
        }
        hpSlider.value = targetValue;
    }
}