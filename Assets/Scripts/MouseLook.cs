using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
    
    public static float defaultMouseSensitivity = 2000f;
    public static float mouseSensitivity = 2000f;

    public Transform playerBody;

    float xRotation = 0f;

    void Update() {

        if(Pause.paused) {
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation , 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
        
    }
    
}