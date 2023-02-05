using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    protected Transform cameraTransform;

    protected Rigidbody2D rigidBody2D;
    protected MouseKeyboardController controller;
    protected Vector3 cameraVelocity;
    protected LineRenderer lineRenderer;

    protected bool m_rooting;

    // Start is called before the first frame update
    void Start()
    {
        m_rooting = false;
        cameraVelocity = new Vector3();
        lineRenderer = GetComponent<LineRenderer>();
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

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, cameraTransform.position);

        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), 0);
        rigidBody2D.AddForce(move);

        if (controller.GetPressedDown() && m_rooting == false)
        {
            Root();
            m_rooting = true;
        }
    }

    private void Root()
    {
        Vector2 direction = new Vector2(controller.GetPositionInWorld().x - transform.position.x, controller.GetPositionInWorld().y - transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, 10.0f);

        rigidBody2D.AddForce(direction.normalized * 20.0f, ForceMode2D.Impulse);
    }

    public Vector3 GetCameraVelocity()
    {
        cameraVelocity = new Vector3(-cameraVelocity.x, -cameraVelocity.y, cameraVelocity.z);
        return cameraVelocity;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        rigidBody2D.velocity = Vector2.zero;
        m_rooting = false;
    }
}
