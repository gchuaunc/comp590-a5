using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private Transform cameraTransform;

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

        Vector3 translateAmount = (moveVertical * movementSpeed * Time.deltaTime * transform.TransformDirection(Vector3.forward)) +
            (moveHorizontal * movementSpeed * Time.deltaTime * transform.TransformDirection(Vector3.right));
        transform.Translate(translateAmount);

        transform.Rotate(0, mouseX, 0);
        cameraTransform.Rotate(mouseY, 0, 0);
    }
}
