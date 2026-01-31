using UnityEngine;
using UnityEngine.EventSystems;

public class JuicyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 originalScale;
    public float hoverScale = 1.1f;
    public float pressScale = 0.9f;

    void Start() => originalScale = transform.localScale;

    public void OnPointerEnter(PointerEventData eventData) => transform.localScale = originalScale * hoverScale;
    public void OnPointerExit(PointerEventData eventData) => transform.localScale = originalScale;
    public void OnPointerDown(PointerEventData eventData) => transform.localScale = originalScale * pressScale;
    public void OnPointerUp(PointerEventData eventData) => transform.localScale = originalScale * hoverScale;
}