using UnityEngine;
// Hierarchy
// Player (Transform - Rigidbody - Collider)
//      -> Camera
public class FPSimpleController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed;
    public float runSpeed;
    public float smoothMovement;

    [Header("Camera")]
    public Transform cameraTransform;
    public float sensitivity;
    public float smooth;

    Vector2 mouseRotation = Vector2.zero;

    protected CharacterInput Input;
    protected Rigidbody Rigidbody;

    private void Start()
    {
        Input = gameObject.AddComponent<CharacterInput>();
        Rigidbody = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Mouse Look
        mouseRotation.x += -Input.CameraAxis.y * sensitivity * Time.deltaTime;
        mouseRotation.y += Input.CameraAxis.x * sensitivity * Time.deltaTime;
        mouseRotation.x = Mathf.Clamp(mouseRotation.x, -90, 90);

        Quaternion rotX = Quaternion.Euler(
            cameraTransform.localRotation.x,
            mouseRotation.y,
            cameraTransform.localRotation.z
        );

        Quaternion rotY = Quaternion.Euler(
            mouseRotation.x,
            transform.localRotation.y,
            transform.localRotation.z
        );

        cameraTransform.localRotation = Quaternion.Slerp(cameraTransform.localRotation, rotY, smooth * Time.deltaTime);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotX, smooth * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        float _currentSpeed = (!Input.Run ? walkSpeed : runSpeed) * Time.fixedDeltaTime;
        Vector3 _moveInput = Input.KeyAxis.normalized;
        Vector3 _moveDirection = transform.right * _moveInput.x + transform.forward * _moveInput.y;
        Vector3 _targetPosition = Vector3.Lerp(Rigidbody.position, Rigidbody.position + _moveDirection * _currentSpeed, smoothMovement * Time.deltaTime);
        Rigidbody.MovePosition(_targetPosition);
    }
