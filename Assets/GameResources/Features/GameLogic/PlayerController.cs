namespace CanvasTest.Features.GameLogic
{
    using System.Collections;
    using TMPro;
    using Unity.VisualScripting;
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;



        [SerializeField] private Transform groundCheckCapsulePos;
        [SerializeField] private Vector2 isGroundedCapsuleCheckSize = new Vector2(0.7f, 0.2f);
        
        [SerializeField] private float speed;
        [SerializeField] private float jumpSpeed;
        private Rigidbody2D body;
        private bool grounded;
        public LayerMask groundLayer;

        private float timer = 0.2f;
        private float currentTimerTime = 0;

        // Start is called before the first frame update
        void Start()
        {
            body = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            // "Restart"
            if (Input.GetKeyDown(KeyCode.Escape)){
                StartCoroutine(restart()); 
            }

            // basic movement
            body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);

            // jump
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded()){
                jump();
            }

            // debugging the 'grounded' state
            currentTimerTime += Time.deltaTime;
            if (currentTimerTime > timer){
                currentTimerTime = 0;
                text.text = "is grounded = " + isGrounded();
            }
        }

        private void jump()
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
            // grounded = false;
        }

        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground"){
                grounded = true;
            }
        }

        private bool isGrounded(){
            print("checking ground");
            return Physics2D.OverlapCapsule(groundCheckCapsulePos.position, isGroundedCapsuleCheckSize, CapsuleDirection2D.Horizontal, 0, groundLayer);
        }



        private IEnumerator restart(){
                body.linearVelocity = Vector2.zero;
                float gravityScale = body.gravityScale;
                body.gravityScale = 0;
                transform.position = Vector3.zero;
                yield return new WaitForSeconds(0.5f);
                body.gravityScale = gravityScale;
        }
    
    }


}
