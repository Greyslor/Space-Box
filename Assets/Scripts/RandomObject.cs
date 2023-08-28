using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObject : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Update()
    {
        if (transform.position.x < minX || transform.position.x > maxX || transform.position.y < minY || transform.position.y > maxY)
        {
            Destroy(gameObject);
        }
    }
}
