using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    float projectileSpeed = 10;
    float travelTime = 2;
    float timeSinceFire;
    int projectileDamage = 10;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleTime();
        MoveProjectile();
    }

    public void ProjectileInit(Vector2 dir){
        transform.rotation = Quaternion.FromToRotation(transform.right, dir);
        
    }

    void MoveProjectile(){
        transform.Translate(new Vector2(1, 0) * projectileSpeed * Time.deltaTime);
    }

    void HandleTime(){
        timeSinceFire += Time.deltaTime;
        if(timeSinceFire >= travelTime){
            Destroy(this.gameObject);
        }
    }

    public int GetProjectileDamage(){
        return projectileDamage;
    }
}
