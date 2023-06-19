using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob : MonoBehaviour
{
    
    GameObject player;
    SpriteRenderer spriteRenderer;
    protected int mobDamage;
    protected float mobSpeed;

    protected int mobHealth;
    protected float currentSpeed;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CheckDead();
        MoveTowardsPlayer();
    }

    protected virtual void MoveTowardsPlayer(){
        Vector2 moveDir = transform.position - player.transform.position;
        spriteRenderer.flipX = moveDir.x < 0;
        moveDir.Normalize();
        //Debug.Log(moveDir);
        transform.Translate(new Vector3 (-moveDir.x, 0, 0) * mobSpeed * Time.deltaTime);
        currentSpeed = Mathf.Abs(moveDir.x);
    }

    protected virtual void TakeDamage(int damage){
        mobHealth -= damage;
    }

    protected virtual void CheckDead(){
        if(mobHealth <= 0){
            Destroy(this.gameObject);
        }
    }

    protected virtual void UpdateAnimation(){

    }
}
