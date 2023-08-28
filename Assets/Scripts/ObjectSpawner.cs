using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public float spawnInterval = 2f;
    public float increaseRate = 0.1f;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private float nextSpawnTime;

    private void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnObject();
            nextSpawnTime = Time.time + spawnInterval;
            spawnInterval *= 1 - increaseRate;
        }
    }

    private void SpawnObject()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);

        // Agrega un Rigidbody2D al objeto generado
        Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Aplica una velocidad aleatoria en X y Y al objeto
            rb.velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        }
    }
}
