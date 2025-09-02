using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : Moveable
{
    private Vector2 velocity;
    [SerializeField]
    private float jumpSpeed = 20;
    public bool onGround = true;
    public bool couldJumpInJump = true;
    private bool isFacingRight = true;
    public static event Action action;

    [Header("Dashing")]
    [SerializeField] 
    private bool canDash = true;
    [SerializeField] 
    private bool isDashing;
    [SerializeField] 
    private float dashSpeed = 24f;
    [SerializeField] 
    private float dashDuration = 0.1f;
    [SerializeField] 
    private float dashCooldown = 1f;
    [SerializeField] 
    private TrailRenderer tr;
    private bool hasJump = false;
    private float jumpImp = 0f;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (hasJump) {
            Jump(jumpImp);
            hasJump = false;
        }
        if (isDashing)
        {
            return;
        }
        Move(velocity);
    }

    public void OnMove(InputValue playerInput)
    {
        velocity = playerInput.Get<Vector2>();
        if (velocity.x < 0)
        {
            isFacingRight = false;
        }
        if (velocity.x > 0)
        { 
            isFacingRight = true;
        }
    }

    public void OnInteract(InputValue playerInput)
    {
        Debug.Log("action");
        action?.Invoke();
    }
    public void OnSprint(InputValue playerInput)
    {
        if (canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        float dashDirectionX = isFacingRight ? 1f : -1f;
        if (velocity.x != 0)
        {
            dashDirectionX = Mathf.Sign(velocity.x);
        }

        rb.linearVelocityX = dashDirectionX * dashSpeed;

        if (tr != null) tr.emitting = true;


        yield return new WaitForSeconds(dashDuration);

        if (tr != null) tr.emitting = false;

        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    public void OnJump(InputValue playerInput)
    {

        float jumpingCoeficient = playerInput.Get<float>();
        if (onGround == true)
        {
            rb.linearVelocityY = 0;
            jumpImp = jumpingCoeficient;
            //Jump(jumpingCoeficient);
            hasJump = true;
            onGround = false;
        }
        else
        {
            if (couldJumpInJump == true)
            {
                if (rb.linearVelocityY > 0)
                {
                    rb.linearVelocityY = 0.5f * rb.linearVelocityY;
                    hasJump = true;
                    jumpImp = jumpingCoeficient;
                    //Jump(jumpingCoeficient);
                }
                else {
                    hasJump = true;
                    jumpImp = Mathf.Abs(rb.linearVelocityY) * 0.1f + 1.1f * jumpingCoeficient;
                    //Jump(Mathf.Abs(rb.linearVelocityY)*0.1f + 1.1f*jumpingCoeficient);
                    }
                    
                couldJumpInJump = false;
            }
        }
     

    }

    public void Jump(float jumpImpulse) 
    {
        rb.AddForce(new Vector2(0,1) * jumpImpulse *Time.fixedDeltaTime * jumpSpeed, ForceMode2D.Impulse);
    }

}
