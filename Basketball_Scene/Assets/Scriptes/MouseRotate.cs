using UnityEngine;

public class MouseRotate : MonoBehaviour
{
    [SerializeField] private InputManager inputs;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float mouseSensitivity = 300f;
    private PlayerStateManager playerState;
    private float yRotation;
    private float xRotation;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        playerState = GetComponent<PlayerStateManager>();
        yRotation = 0f;
    }

    void Update()
    {
        if (!playerState.IsLookAroundBlocked)
        {
            xRotation = inputs.Mouse_X * mouseSensitivity * Time.deltaTime;
            transform.Rotate(Vector3.up, xRotation);
        }

        yRotation -= inputs.Mouse_Y * mouseSensitivity * Time.deltaTime;
        yRotation = Mathf.Clamp(yRotation, -45f, 70f);
        mainCamera.localRotation = Quaternion.Euler(Vector3.right * yRotation);
    }
}
