using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spamPoint;

    float timer;


    void Awake()
    {
        spamPoint = GetComponentsInChildren<Transform>();  
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.2f)
        {
            timer = 0;
            Spawn();
        }

    }
    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(Random.Range(0, 2));
        enemy.transform.position = spamPoint[Random.Range(1, spamPoint.Length)].position;
    }
}
