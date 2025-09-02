using UnityEngine;
using UnityEngine.Rendering;

public class GroundCheck : MonoBehaviour
{
    PlayerController controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if (collision.CompareTag("Ground"))
        {
            controller.onGround = true;
            controller.couldJumpInJump = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            controller.onGround = false;
            
        }

    }
}
    

