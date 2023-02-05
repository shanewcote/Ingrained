using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected Rigidbody2D rigidBody2D;
    protected MouseKeyboardController controller;
    [SerializeField]
    protected Transform cameraTransform;
    protected Vector3 cameraVelocity;

    // Start is called before the first frame update
    void Start()
    {
        cameraVelocity = new Vector3();
        controller = new MouseKeyboardController();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        controller.UpdateController();

        Vector3 pos = (transform.position + controller.GetPositionInWorld()) * 0.5f;
        pos.z = -1.92f;
        cameraVelocity = cameraTransform.position - pos;
        cameraTransform.position = pos;

        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), 0);
        rigidBody2D.AddForce(move);
    }

    public Vector3 GetCameraVelocity()
    {
        cameraVelocity = new Vector3(-cameraVelocity.x, -cameraVelocity.y, cameraVelocity.z);
        return cameraVelocity;
    }
}
