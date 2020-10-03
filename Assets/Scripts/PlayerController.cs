using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public float sensitivity = 1f;
    public float movementSpeedModifier = 1f;
    public bool Enabled { get => _enabled; set { SetEnabled(value); } }
    private bool _enabled = true;

    private Rigidbody rb;
    private Transform cam;
    private Vector2 mousePos;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>().transform;
        rb = GetComponent<Rigidbody>();
        Instance = this;
    }

    private void Start()
    {
        SetEnabled(_enabled);
    }

    void FixedUpdate()
    {
        if (_enabled)
        {
            // CAMERA
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            mousePos += mouseDelta * new Vector2(1, -1) * sensitivity * Time.fixedDeltaTime;

            cam.localRotation = Quaternion.Euler(mousePos.y, mousePos.x, 0);

            // MOVEMENT
            Vector3 move = cam.forward * Input.GetAxisRaw("Vertical") * movementSpeedModifier * Time.fixedDeltaTime;
            Vector3 strafe = cam.right * Input.GetAxisRaw("Horizontal") * movementSpeedModifier * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move + strafe);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetEnabled(!_enabled);
        }
    }

    private void SetEnabled(bool enabled)
    {
        _enabled = enabled;
        Cursor.visible = !enabled;
        Cursor.lockState = enabled ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
