using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    public GameObject projectile;
    public float kickForce = 5;
    public float speed = 5;
    public float INITIAL_VELOCITY_Y = 10;
    private float initialVelocityX = 0;
    public float idleThreshould = 1;
    public float fireRate = 100;    // Value of 100 corresponds to 1 shot per second
    private float timeSinceLastFire = 0;

    private Rigidbody2D rb;
    private Rigidbody2D projectileRB;
    private Vector2 previousPosition;
    private float notMovingTime = 0;
    private int direction = 1;
    private GameObject fireTarget;
    private Vector2 firePositionOffset = new(0, 5);

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            // print("Player's velocity before is: " + other.gameObject.GetComponent<Rigidbody2D>().velocity);
            // Vector2 velocity = other.attachedRigidbody.velocity;
            // Vector2 velocity = other.gameObject.GetComponent<Rigidbody2D>().velocity;
            // velocity.x += kickForce;
            // other.attachedRigidbody.velocity = velocity;
            // other.gameObject.GetComponent<Rigidbody2D>().velocity = velocity;

            other.attachedRigidbody.AddForce(new Vector2(50, 50), ForceMode2D.Impulse);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        print("The frog was born!");
        rb = GetComponent<Rigidbody2D>();
        fireTarget = GameObject.FindGameObjectWithTag("Player");
        projectileRB = projectile.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        fire();
    }


    private void fire()
    {
        float g = Physics2D.gravity.y * projectileRB.gravityScale;
        float x = fireTarget.transform.position.x - (rb.position.x + firePositionOffset.x);
        float y = fireTarget.transform.position.y - (rb.position.y + firePositionOffset.y);
        float denominator = -INITIAL_VELOCITY_Y - Mathf.Sqrt(INITIAL_VELOCITY_Y * INITIAL_VELOCITY_Y + 2 * g * y);

        if (timeSinceLastFire > 100 / fireRate && !Double.IsNaN(denominator))
        {
            timeSinceLastFire = 0;

            GameObject newProjectile = Instantiate(projectile, rb.position + firePositionOffset, Quaternion.identity);
            Rigidbody2D newProjectileRB = newProjectile.GetComponent<Rigidbody2D>();

            if (!Double.IsNaN(denominator))
            {
                initialVelocityX = x * g / denominator;
            }
            else
            {
                initialVelocityX = 0;
            }
            newProjectileRB.linearVelocity = new Vector2(initialVelocityX, INITIAL_VELOCITY_Y);
        }
        else
        {
            timeSinceLastFire += Time.deltaTime;
        }
    }

    private void move()
    {
        if (rb.position == previousPosition)
        {
            notMovingTime += Time.deltaTime;
            if (notMovingTime > idleThreshould)
            {
                direction *= -1;
            }
        }
        else
        {
            notMovingTime = 0;
        }

        rb.linearVelocity = new Vector2(speed * direction, 0);
        previousPosition = rb.position;
    }
}
