using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int playerHealth = 100;
    int playerDamage = 10;
    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float jumpForce = 2.5f;
    SpriteRenderer spriteRenderer;

    Animator animator;

    Rigidbody2D rb2d;
    bool canJump = true;
    bool inAir = false;
    //float jumpDelay = .5f;

    [SerializeField] GameObject projectile;
    float projectileDelay = 1f;
    float timeSinceLastProjectile;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        FireProjectile();
        UpdateAnimation();
        if(timeSinceLastProjectile < projectileDelay){
            timeSinceLastProjectile += Time.deltaTime;
        }
    }

    void MovePlayer(){
        if (Input.GetKey(KeyCode.A))
        {
            spriteRenderer.flipX = true;
            transform.Translate(new Vector3(-playerSpeed, 0, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            
            spriteRenderer.flipX = false;
            transform.Translate(new Vector3(playerSpeed, 0, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            PlayerJump();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ground"){
            canJump = true;
            inAir = false;            
        }
    }

    void UpdateAnimation(){
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && !inAir){
            animator.SetBool("isRunning", true);
        } else {
            animator.SetBool("isRunning", false);
        }
        if(inAir){
            animator.SetBool("playerInAir", true);
        } else {
            animator.SetBool("playerInAir", false);
        }
    }

    void PlayerJump(){
        if(canJump){
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
            inAir = true;
        }
    }

    void FireProjectile(){
        if(timeSinceLastProjectile > projectileDelay){
            if(Input.GetMouseButton(0)){
                animator.SetTrigger("playerAttack");
                Vector2 projectileDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                spriteRenderer.flipX = projectileDir.x < 0;
                float projectileAngle = Vector2.Angle(projectileDir, new Vector2(1, 0));
                projectileDir.Normalize();
                //Debug.Log(projectileDir);
                GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                newProjectile.GetComponent<PlayerProjectile>().ProjectileInit(projectileDir);
                timeSinceLastProjectile = 0;
                //spriteRenderer.sprite = playerNormal;
            } 
        }
    }

    public int GetPlayerDamage(){
        return playerDamage;
    }

    public void TakeDamage(int damage){
        playerHealth -= damage;
    }
}
