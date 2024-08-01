using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("Spawn", 0, spawnRate);


    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            Spawnn();
            timer = 0;
        }
    }

    void Spawnn()
    {
        Instantiate(enemyPrefab, transform.position, transform.rotation);

    }
}