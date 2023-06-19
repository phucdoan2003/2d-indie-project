using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Mob
{
    int boarHealth = 50;
    int boarDamage = 10;
    float boarSpeed = 5f;
    bool isWalking;
    bool isRunning;

    Animator animator;
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        mobDamage = boarDamage;
        mobHealth = boarHealth;
        mobSpeed = boarSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDead();
        UpdateAnimation();
        MoveTowardsPlayer();
    }

    protected override void MoveTowardsPlayer()
    {
        base.MoveTowardsPlayer();
        if(currentSpeed > 0.5){
            isRunning = true;
            isWalking = false;
        } else if (currentSpeed > 0){
            isRunning = false;
            isWalking = true;
        }
        Debug.Log(currentSpeed);
    }

    protected override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        animator.SetTrigger("takeDamage");
        Debug.Log("Boar took " + damage + " damage");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Trigger");
        if(other.tag == "PlayerProjectile"){
            TakeDamage(other.gameObject.GetComponent<PlayerProjectile>().GetProjectileDamage());
        }
    }

    protected override void CheckDead()
    {
        base.CheckDead();
    }

    protected override void UpdateAnimation()
    {
        if(isWalking){
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        } else if(isRunning){
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
        } else {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
        }
    }
}
