using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    public Transform cameraTransform;

    // Тряска камеры
    public float bobSpeed = 140f;
    public float bobAmountY = 0.5f;
    public float bobAmountX = 0.5f;
    private float bobTimer = 1f;
    private Vector3 originalCamPos;

    private CharacterController controller;
    private Vector3 velocity;
    private float verticalLookRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        // Сохраняем изначальное положение камеры
        originalCamPos = cameraTransform.localPosition;
    }

    void Update()
    {
        // Управление мышью
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -85f, 85f);
        cameraTransform.localRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);

        // Управление движением
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Прыжок и гравитация
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

    //    if (Input.GetButtonDown("Jump") && controller.isGrounded)
         //   velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Тряска камеры при движении
        if (controller.isGrounded && move.magnitude > 0.1f)
        {
            bobTimer += Time.deltaTime * bobSpeed;
            float bobY = Mathf.Sin(bobTimer) * bobAmountY;
            float bobX = Mathf.Cos(bobTimer * 0.5f) * bobAmountX;

            cameraTransform.localPosition = originalCamPos + new Vector3(bobX, bobY, 0f);
        }
        else
        {
            // Плавное возвращение камеры на место
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, originalCamPos, Time.deltaTime * 5f);
        }
    }
}
