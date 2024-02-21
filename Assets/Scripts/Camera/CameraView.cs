using System.Collections;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    private Vector3 originalPosition;
    [SerializeField] private float defaultShakeDuration;
    [SerializeField] private float defaultShakeMagnitude;

    private float sensitivity = 1f;
    private const float rotationLimit = 0.5f;

    private void Start() => originalPosition = transform.localPosition;

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
}
