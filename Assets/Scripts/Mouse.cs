using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{

    public float mouseSensitivity = 100f;

    public float topClamp = -90f;
    public float bottomClamp = 90f;

    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {
        //lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //getting mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;    

        //rotation around x-axis (look up and down)
        xRotation -= mouseY;
        
        //rotation around y-axis (look up and down)
        yRotation += mouseX;

        //clamp rotation {means"to hold two things together tightly"}
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        //Apply rotation to transform {body of player}
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

    }
}
