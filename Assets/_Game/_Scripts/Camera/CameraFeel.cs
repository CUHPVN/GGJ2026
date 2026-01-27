using System.Collections;
using UnityEngine;

public class CameraFeel : MonoBehaviour
{
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private SlotMachine slotMachine;

    [SerializeField] private GameObject chromaVolume;
    public void OnEnable()
    {
        if (slotMachine != null)
        {
            slotMachine.ShakeCamera += cameraShake.StartShake;
            slotMachine.ShakeCamera += StartChroma;
        }
        else
        {
            Debug.LogWarning("SlotMachine is not ref");
        }
    }
    public void StartChroma()
    {
        StartCoroutine(Chroma());
    }
    private IEnumerator Chroma()
    {
        chromaVolume.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        chromaVolume.SetActive(false);
    }
    public void OnDisable()
    {
        if (slotMachine != null)
        {
            slotMachine.ShakeCamera -= cameraShake.StartShake;
            slotMachine.ShakeCamera -= StartChroma;
        }
    }
    public void OnDestroy()
    {
        StopAllCoroutines();
    }
}
