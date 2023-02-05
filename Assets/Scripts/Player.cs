using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    protected Transform cameraTransform;

    [SerializeField]
    protected Transform spriteTransform;

    protected Rigidbody2D rigidBody2D;
    protected MouseKeyboardController controller;
    protected Vector3 cameraVelocity;
    protected LineRenderer lineRenderer;
    protected Animator animator;

    protected bool m_drilling;
    protected bool m_collision;
    protected bool m_idle;
    protected Vector3 m_lastPosition;
    protected Quaternion m_lastRotation;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = m_lastPosition;
        spriteTransform.rotation = m_lastRotation;

        m_idle = true;
        m_drilling = false;
        m_collision = false;
        cameraVelocity = new Vector3();
        lineRenderer = GetComponent<LineRenderer>();
        controller = new MouseKeyboardController();
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_idle = animator.GetCurrentAnimatorStateInfo(0).IsName("idle");
        animator.SetBool("Drilling", m_drilling);
        animator.SetBool("Collision", m_collision);
        controller.UpdateController();

        Vector3 pos = (transform.position + controller.GetPositionInWorld()) * 0.5f;
        pos.z = -1.92f;
        cameraVelocity = cameraTransform.position - pos;
        cameraTransform.position = pos;

        Vector2 direction = new Vector2(controller.GetPositionInWorld().x - transform.position.x, controller.GetPositionInWorld().y - transform.position.y);

        LayerMask mask = LayerMask.GetMask("Default");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, 200.0f, mask);
        Vector3 impact = new Vector3(hit.point.x, hit.point.y, transform.position.z);

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }

        if (m_idle)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
        }
        else
        {
            lineRenderer.enabled = false;
        }

        if (hit.point != Vector2.zero)
        {
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, controller.GetPositionInWorld());
        }

        if (controller.GetPressedDown() && m_drilling == false && m_idle)
        {
            m_drilling = true;
            Root();
        }

        if (m_drilling == true)
        {
            spriteTransform.rotation = Quaternion.FromToRotation(-Vector3.up, rigidBody2D.velocity.normalized);
        }
    }

    private void Root()
    {
        Vector2 direction = new Vector2(controller.GetPositionInWorld().x - transform.position.x, controller.GetPositionInWorld().y - transform.position.y);
        rigidBody2D.AddForce(direction.normalized * 20.0f, ForceMode2D.Impulse);

        m_collision = false;
    }

    public Vector3 GetCameraVelocity()
    {
        cameraVelocity = new Vector3(-cameraVelocity.x, -cameraVelocity.y, cameraVelocity.z);
        return cameraVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            rigidBody2D.velocity = Vector2.zero;

            m_drilling = false;
            m_collision = true;

            spriteTransform.rotation = Quaternion.FromToRotation(Vector3.up, collision.GetContact(0).normal);

            m_lastRotation = spriteTransform.rotation;
            m_lastPosition = transform.position;
        }
    }

    protected void Reset()
    {
        rigidBody2D.velocity = Vector2.zero;
        transform.position = m_lastPosition;
        spriteTransform.rotation = m_lastRotation;
        animator.Play("idle");
    }
}
