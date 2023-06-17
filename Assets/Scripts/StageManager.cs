using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject groundPrefab;
    [SerializeField] GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GenerateGround();
        GeneratePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateGround(){
        Instantiate(groundPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    void GeneratePlayer(){
        Instantiate(playerPrefab, new Vector3(0, 3, 0), Quaternion.identity);
    }
}
