using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] private Transform body;
    [SerializeField] private Transform head;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private InteractableRaycaster raycaster;

    [SerializeField] private float speed;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float jumpForce;

    //Pitch tracker
    [SerializeField] private float maxPitch;
    [SerializeField] private float minPitch;
    public float pitch = 0;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        //Movement
        float forward = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float side = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        //Orientation
        float dpitch = -Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        float dyaw = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        //Clamp pitch between max & min pitch
        if (pitch + dpitch > maxPitch)
        {
            dpitch = maxPitch - pitch;
        }

        if (pitch + dpitch < minPitch)
        {
            dpitch = minPitch - pitch;
        }
        pitch += dpitch;

        if (body)
        {
            body.Translate(new Vector3(side, 0, forward));
            body.Rotate(new Vector3(0, dyaw, 0));
        }

        if (head)
        {
            head.Rotate(new Vector3(dpitch, 0, 0));
        }

        //Jump
        if (body && rigidBody && Input.GetKey(KeyCode.Space))
        {
            if (IsGrounded())
            {
                rigidBody.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            }
        }

        //Interact
        if (Input.GetKey(KeyCode.E))
        {
            if (raycaster && raycaster.Target != null) raycaster.InteractWithTarget();
        }
    }

    public bool IsGrounded()
    {
        Ray ray = new Ray(body.position, -body.up);
        bool grounded = Physics.Raycast(ray, 1);
        return grounded;
    }
}
