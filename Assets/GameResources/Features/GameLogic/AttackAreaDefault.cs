using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackAreaDefault : MonoBehaviour
{

    [SerializeField] private int damage = 5;

    private void OnTriggerEnter2D(Collider2D collider){
        Health health = collider.GetComponent<Health>();
        Debug.Log("in attack trigger");
        if (health != null){
            Debug.Log("dealing " + damage + " damage to " + collider.name);
            health.Damage(damage);
        }
        else{
            Debug.Log("can't hit " + collider.name);
        }
        
    }

}
