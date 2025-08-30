using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour_1 : MonoBehaviour
{
    private int damage = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject hitObject = other.gameObject;

        if (hitObject.CompareTag("Player"))
        {
            // print("bullet hit player");
            Health hitObjectHealth = hitObject.GetComponent<Health>();
            if (hitObjectHealth)
            {
                hitObjectHealth.Damage(damage);
            }
            else
            {
                // print("no health component on player");
            }

            Destroy(gameObject);
        }
        else if (hitObject.CompareTag("Ground"))
        {
            // print("bullet hit ground");
            Destroy(gameObject);
        }
        else
        {
            // print("bullet hit " + hitObject.name);

        }
    }

}
