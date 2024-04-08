using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCam : MonoBehaviour
{
    float mouseX;
    float mouseY;
    float rotX;
    float rotY;

    [SerializeField] float sensX;
    [SerializeField] float sensY;
    [SerializeField] GameObject playerl;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        takingRotating();
        rotatingCamera();

    }

    void takingRotating()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * sensX * Time.deltaTime;
        mouseY = Input.GetAxisRaw("Mouse Y") * sensY * Time.deltaTime;

        rotX -= mouseY;
        rotY += mouseX;

        rotX = Mathf.Clamp(rotX, -75f, 70f);
    }

    void rotatingCamera()
    {
        transform.rotation = Quaternion.Euler(rotX, rotY, 0);
        playerl.transform.rotation = Quaternion.Euler(transform.rotation.x, rotY, transform.rotation.z);
    }



}
