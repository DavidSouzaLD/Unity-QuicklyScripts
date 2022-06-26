using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private bool invertedCamera;
    [SerializeField] private float sensitivity = 3f;

    Vector2 _mouseRotation = Vector2.zero;

    private void Start()
    {
        _mouseRotation.y = rotTransformY;
        _mouseRotation.x = rotCameraX;

        // Lock mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Mouse Look
        Vector2 _cameraAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // Easy to implement new input system
        float _speed = Time.deltaTime * sensitivity;

        _mouseRotation.x += _cameraAxis.x * _speed;
        _mouseRotation.y += (invertedCamera ? _cameraAxis.y : -_cameraAxis.y) * _speed;
        _mouseRotation.y = Mathf.Clamp(_mouseRotation.y, -80, 80); // Limit vertical angle

        // Rotate camera to vertical
        cameraTransform.localRotation = Quaternion.Euler(
            _mouseRotation.y,
            cameraTransform.localRotation.y,
            cameraTransform.localRotation.z);

        // Rotate body to horizontal
        tranform.localRotation = Quaternion.Euler(
            tranform.localRotation.x,
            _mouseRotation.x,
            tranform.localRotation.z);
    }
}
