using System;
using System.Collections;
// using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject attackArea;
    [SerializeField] private float speed;
    [SerializeField] private Transform groundCheckCapsulePos;
    [SerializeField] private Vector2 isGroundedCapsuleCheckSize = new Vector2(0, 0);
    public LayerMask groundLayer;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float timeToAttack = 0.2f;


    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool is_grounded = true;
    public Vector2 hookVisualAnchor = Vector2.zero;
    public Vector2 hookVisualOffset = Vector2.zero;
    public float comboTime = 0.5f;

    void Start()
    {
        // animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }


    void Update()
    {
        // basic movement
        rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.linearVelocity.y);
        // rb.velocity = new Vector2(rb.velocity.x + Input.GetAxis("Horizontal") * speed, rb.velocity.y);

    }


    private void OnJump()
    {
        if (isGrounded())
        {
            jump();
        }
    }

    private void OnRestart()
    {
        StartCoroutine(restart());
    }


    private bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheckCapsulePos.position, isGroundedCapsuleCheckSize, CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

    private void jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
        is_grounded = false;
    }


    public IEnumerator restart()
    {
        GetComponent<Health>().setHealth(50);
        rb.linearVelocity = Vector2.zero;
        // store previous gravity scale
        float gravityScale = rb.gravityScale;
        // fly above the ground a bit
        rb.gravityScale = 0;
        transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
        // return to previous gravity scale
        rb.gravityScale = gravityScale;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground"))
        {
            is_grounded = true;
            // animator.SetBool("Grounded", true);
        }
    }
}
