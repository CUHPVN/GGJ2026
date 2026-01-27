using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = 0.25f;
    public float magnitude = 0.1f;
    
    public void StartShake()
    {
        StartCoroutine(Shake(duration, magnitude));
    }
    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;
        Vector3 deltaPos;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            deltaPos = originalPos;
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-2f, 1f) * magnitude;
            transform.position = new Vector3(deltaPos.x + x, deltaPos.y + y, deltaPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPos;
    }
    public void OnDestroy()
    {
        StopAllCoroutines();
    }
}
