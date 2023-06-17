using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float bulletSpeed = 10;
    float travelTime = 2;
    float timeSinceFire;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleTime();
        MoveBullet();
    }

    public void BulletInit(Vector2 dir){
        transform.rotation = Quaternion.FromToRotation(transform.right, dir);
        
    }

    void MoveBullet(){
        transform.Translate(new Vector2(1, 0) * bulletSpeed * Time.deltaTime);
    }

    void HandleTime(){
        timeSinceFire += Time.deltaTime;
        if(timeSinceFire >= travelTime){
            Destroy(this.gameObject);
        }
    }
}
