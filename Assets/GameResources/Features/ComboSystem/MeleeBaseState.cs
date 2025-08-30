using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBaseState : State
{
    // How long this state should be active for before moving on
    public float duration;
    // Cached animator component
    protected Animator animator;
    protected Animator slashAnimator;
    // bool to check whether or not the next attack in the sequence should be played or not
    protected bool shouldCombo;
    // The attack index in the sequence of attacks
    protected int attackIndex;
    protected int damage;



    // The cached hit collider component of this attack
    protected Collider2D hitCollider;
    // Cached already struck objects of said attack to avoid overlapping attacks on same target
    private List<Collider2D> collidersDamaged;
    // The Hit Effect to Spawn on the afflicted Enemy
    private GameObject HitEffectPrefab;

    // Input buffer Timer
    private float AttackPressedTimer = 0;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        animator = GetComponent<Animator>();
        slashAnimator = GetComponent<ComboCharacter>().SlashController.GetComponent<Animator>();
        collidersDamaged = new List<Collider2D>();
        hitCollider = GetComponent<ComboCharacter>().hitbox;
        HitEffectPrefab = GetComponent<ComboCharacter>().HitEffect;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        AttackPressedTimer -= Time.deltaTime;

        if (animator.GetFloat("Weapon.Active") > 0f)
        {
            Attack();
        }


        if (Input.GetMouseButtonDown(0))
        {
            AttackPressedTimer = 2;
        }

        // if (animator.GetFloat("AttackWindow.Open") > 0f && AttackPressedTimer > 0)
        if (AttackPressedTimer > 0)
        {
            shouldCombo = true;
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    protected void Attack()
    {
        Collider2D[] collidersToDamage = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;

        int colliderCount = Physics2D.OverlapCollider(hitCollider, filter, collidersToDamage);
        for (int i = 0; i < colliderCount; i++)
        {

            if (!collidersDamaged.Contains(collidersToDamage[i]))
            {
                TeamComponent hitTeamComponent = collidersToDamage[i].GetComponentInChildren<TeamComponent>();

                // Only check colliders with a valid Team Componnent attached
                if (hitTeamComponent && hitTeamComponent.teamIndex == TeamIndex.Enemy)
                {
                    // GameObject.Instantiate(HitEffectPrefab, collidersToDamage[i].transform);
                    GameObject.Instantiate(HitEffectPrefab, collidersToDamage[i].transform.position + new Vector3(0, 5, 0), Quaternion.identity);
                    Debug.Log("Enemy Has Taken: " + damage + " Damage");
                    Health enemyHealth = collidersToDamage[i].GetComponentInChildren<Health>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.Damage(damage);
                    }
                    collidersDamaged.Add(collidersToDamage[i]);
                }
            }
        }
    }

}