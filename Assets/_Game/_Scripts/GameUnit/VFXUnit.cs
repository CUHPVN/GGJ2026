using System.Collections;
using UnityEngine;

public class VFXUnit : GameUnit
{
    [SerializeField] private ParticleSystem ParticleSystem;
    private void OnEnable()
    {
        ParticleSystem.Play();
        StartCoroutine(Disable()); 
    }
    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(1.01f);
        SimplePool.Despawn(this);
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
