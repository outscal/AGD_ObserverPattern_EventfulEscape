using System.Collections;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    private Vector3 originalPosition;
    [SerializeField] private float defaultShakeDuration;
    [SerializeField] private float defaultShakeMagnitude;

    private float sensitivity = 1f;
    private const float rotationLimit = 0.5f;
    private Coroutine shakeCorroutine;

    private void OnEnable()
    {
        EventService.Instance.OnLightsOffByGhostEvent.AddListener(Shake);
        EventService.Instance.OnPlayerDeathEvent.AddListener(Shake);
        EventService.Instance.OnRatRush.AddListener(Shake);
    }

    private void OnDisable()
    {
        EventService.Instance.OnLightsOffByGhostEvent.RemoveListener(Shake);
        EventService.Instance.OnPlayerDeathEvent.RemoveListener(Shake);
        EventService.Instance.OnRatRush.RemoveListener(Shake);
    }

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        Cursor.visible = false;
        cameraMovement();
    }

    private void cameraMovement()
    {
        float velocity = sensitivity * -Input.GetAxis("Mouse Y");
        transform.Rotate(velocity, 0f, 0f);

        float rotationX = transform.localRotation.x;
        if (rotationX > rotationLimit || rotationX < -rotationLimit)
        {
            transform.Rotate(-velocity, 0, 0);
        }
    }

    private void Shake()
    {
        if (shakeCorroutine != null)
            stopCoroutine(shakeCorroutine);
        shakeCorroutine = StartCoroutine(ShakeCoroutine(defaultShakeDuration, defaultShakeMagnitude));
    }

    private IEnumerator ShakeCoroutine(float shakeDuration, float shakeMagnitude)
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float x = Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = Random.Range(-shakeMagnitude, shakeMagnitude);
            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y, originalPosition.z);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
        stopCoroutine(shakeCorroutine);
    }

    private void stopCoroutine(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
        coroutine = null;
    }
}
