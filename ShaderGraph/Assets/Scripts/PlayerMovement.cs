using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CharacterController characterController;

    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        Vector3 translateAmount = (moveVertical * movementSpeed * Time.deltaTime * transform.forward) +
            (moveHorizontal * movementSpeed * Time.deltaTime * transform.right);
        characterController.Move(translateAmount);

        xRotation += mouseY * mouseSensitivity * Time.deltaTime;
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(0f, mouseX * mouseSensitivity * Time.deltaTime, 0f);
    }
}
