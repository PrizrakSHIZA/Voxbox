using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mousesensativity = 100f;
    public Transform PlayerBody;


    float Xrotation = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mousesensativity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mousesensativity * Time.deltaTime;

        if (Mathf.Abs(mouseX) > 20 || Mathf.Abs(mouseY) > 20)
            return;

        Xrotation -= mouseY;
        Xrotation = Mathf.Clamp(Xrotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(Xrotation, 0, 0);
        PlayerBody.Rotate(Vector3.up * mouseX);
    }
}
