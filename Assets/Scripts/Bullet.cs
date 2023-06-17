using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float bulletSpeed = 10;
    float travelTime = 2;
    float timeSinceFire;
    int xDir;
    int yDir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleTime();
        MoveBullet(xDir, yDir);
    }

    public void BulletInit(int x, int y){
        xDir = x;
        yDir = y;
    }

    void MoveBullet(int x, int y){
        transform.Translate(new Vector3(x, y, 0) * bulletSpeed * Time.deltaTime);
    }

    void HandleTime(){
        timeSinceFire += Time.deltaTime;
        if(timeSinceFire >= travelTime){
            Destroy(this.gameObject);
        }
    }
}
