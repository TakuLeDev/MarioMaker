using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("L/R mouvment")]
    public float moveSpeed;
    float horizontalMovment;

    [Header("jump")]
    public float jumpForce;

    [Header("ground check")]
    public Transform groundCheckPos;
    public Vector2 grousndCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(horizontalMovment*moveSpeed, rb.linearVelocity.y);

    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovment = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded() != true) return;
        if (context.performed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        if (context.canceled)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, grousndCheckSize, 0, groundLayer))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckPos.position, grousndCheckSize);
    }
}