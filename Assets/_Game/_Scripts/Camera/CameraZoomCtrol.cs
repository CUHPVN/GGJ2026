using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraZoomCtrol : MonoBehaviour
{
    public AnimationCurve curve;
    [SerializeField] private Camera camera;
    public CameraNeedToZoom zoomToRoll;
    public CameraNeedToZoom zoomToBet;
    private Coroutine coroutine;

    private void OnEnable()
    {
        StateController.Instance.OnEnterStateRoll += ZoomToRoll;  
        StateController.Instance.OnEnterStateBet += ZoomToBet;
    }
    private void OnDisable()
    {
        if (StateController.Instance != null)
        {
            StateController.Instance.OnEnterStateRoll -= ZoomToRoll;
            StateController.Instance.OnEnterStateBet -= ZoomToBet;
        }
    }
    private void ZoomToRoll()
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(Zoom(zoomToRoll));
        }
    }
    private void ZoomToBet()
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(Zoom(zoomToBet));
        }
    }
    private IEnumerator Zoom(CameraNeedToZoom zoom)
    {
        float time = 0;
        float duration = 0.5f;
        Vector3 startPos = camera.transform.position;
        Vector3 targetPos = zoom.pos;
        float startSize = camera.orthographicSize;
        float targetSize = zoom.Size;
        while (time<duration)
        {
            time += Time.deltaTime;
            camera.transform.position = Vector3.Lerp(startPos, targetPos, curve.Evaluate(time / duration));
            camera.orthographicSize = Mathf.Lerp(startSize, targetSize, curve.Evaluate(time / duration));
            yield return null;
        }
        camera.transform.position = targetPos;
        camera.orthographicSize = targetSize;
        coroutine = null;
    }
    [System.Serializable]
    public struct CameraNeedToZoom
    {
        public Vector3 pos;
        public float Size;

    };
}
