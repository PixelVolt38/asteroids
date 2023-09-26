using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{

    public GameObject asteroidPrefab;
    public float spawnRate = 30f;
    public float spawnRateIncrement = 1f;
    public float timeLimit = 1.5f;

    public float xLimit = 9f;
    public float meteorDiff = 10f;

    private float spawnNext = 0;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnNext)
        {
            spawnNext = Time.time + 60 / spawnRate;

            spawnRate += spawnRateIncrement;

            float random = Random.Range(-xLimit,xLimit);

            genMeteor(new Vector3(random, 7f, 5f));
            
        }
    }

    void genMeteor(Vector3 spawnPosition)
    {
        GameObject meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        Destroy(meteor, timeLimit);
    }

    public void genSons(GameObject meteor)
    {
        var position = meteor.transform.position;
        genMeteor(new Vector3(position.x, position.y-meteorDiff, position.z));
        genMeteor(new Vector3(position.x, position.y+meteorDiff, position.z));
    }
}
