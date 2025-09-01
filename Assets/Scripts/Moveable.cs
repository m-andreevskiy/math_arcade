using UnityEngine;

public class Moveable : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody2D rb;
    [SerializeField]
    private float speed = 140;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Move(Vector2 vec)
    {
        rb.linearVelocityX = vec.x * speed * Time.fixedDeltaTime;
;    }
}
