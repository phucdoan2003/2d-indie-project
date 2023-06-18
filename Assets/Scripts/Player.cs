using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float jumpForce = 2.5f;
    SpriteRenderer spriteRenderer;

    Animator animator;
    Rigidbody2D rb2d;
    bool canJump = true;
    //float jumpDelay = .5f;

    [SerializeField] GameObject bullet;
    float bulletDelay = 0.25f;
    float timeSinceLastBullet;

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
        FireBullet();
        UpdateAnimation();
        if(timeSinceLastBullet < bulletDelay){
            timeSinceLastBullet += Time.deltaTime;
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
        }
    }

    void UpdateAnimation(){
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
            animator.SetBool("isRunning", true);
        } else {
            animator.SetBool("isRunning", false);
        }
    }

    void PlayerJump(){
        if(canJump){
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
        }
    }

    void FireBullet(){
        if(timeSinceLastBullet > bulletDelay){
            if(Input.GetMouseButton(0)){
                Vector2 bulletDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                spriteRenderer.flipX = bulletDir.x < 0? true : false;
                float bulletAngle = Vector2.Angle(bulletDir, new Vector2(1, 0));
                bulletDir.Normalize();
                Debug.Log(bulletDir);
                GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                newBullet.GetComponent<Bullet>().BulletInit(bulletDir);
                timeSinceLastBullet = 0;
                //spriteRenderer.sprite = playerNormal;
            } 
        }
    }


}
