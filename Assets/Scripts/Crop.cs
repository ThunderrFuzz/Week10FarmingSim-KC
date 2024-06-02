using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public float growTime;
    public float ripenTime;
    public float spoilTime;
    public int value;
    public bool isRipe;

    private float currentTime;
    private Vector3 initialScale;
    private Vector3 ripeScale;

    public GameObject fruitPrefab; // Fruit prefab to instantiate
    public GameObject[] fruitSpawnPoints; // Array of spawn points for the fruit

    void Start()
    {
        currentTime = 0f;
        isRipe = false;
        initialScale = transform.localScale * 0.3f;
        ripeScale = transform.localScale * 2.0f;
        transform.localScale = initialScale;
    }

    public void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime < growTime)
        {
            Grow();
        }
        else if (currentTime < growTime + ripenTime)
        {
           
            if (!isRipe)
            {
                isRipe = true;
                transform.localScale = ripeScale;
                InstantiateFruits();
            }
        }
        else if (currentTime < growTime + ripenTime + spoilTime)
        {
            Spoil();
        }
        
    }

    void Grow()
    {
        float t = currentTime / growTime;
        transform.localScale = Vector3.Lerp(initialScale, ripeScale, t);
    }

  

    void Spoil()
    {
        Destroy(gameObject);
    }

    public void Harvest()
    {
        if (isRipe)
        {
            // Add score logic here if needed
            Destroy(gameObject);
        }
    }

    void InstantiateFruits()
    {
        foreach (GameObject spawnPoint in fruitSpawnPoints)
        {
            Instantiate(fruitPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation, transform);
        }
    }
}
