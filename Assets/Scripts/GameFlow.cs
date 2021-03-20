using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public Transform mainTileObj;
    public int spawnSpeed = 1;
    public int mainTileWidth = 4;
    public int mainTileLength = 12;
    public int startingTiles = 5;
    public GameObject[] spawnables;
    public float spawnTileRate = 3.0f;
    public float[] spawnRates;
    public float noSpawnRate;
    public float spawnEnemiesY = 1.5f;

    private Vector3 nextTileSpawn;
    private Vector3 spawnPoint;
    private float midPoint;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Make sure spawnables and spawnrates are of same size.");
        nextTileSpawn.z = mainTileWidth * startingTiles;
        spawnPoint = nextTileSpawn;
        midPoint = mainTileLength / 4;
        StartCoroutine(spawnTile());
        StartCoroutine(spawnSpawnables());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnTile()
    {
        yield return new WaitForSeconds(spawnSpeed);
        Instantiate(mainTileObj, nextTileSpawn, mainTileObj.rotation);
        nextTileSpawn.z += mainTileWidth;
        StartCoroutine(spawnTile());
    }

    IEnumerator spawnSpawnables()
    {
        yield return new WaitForSeconds(spawnSpeed);
        float spawnTile = getRandom(spawnTileRate);
        spawnPoint = nextTileSpawn;
        spawnPoint.y = spawnEnemiesY;
        spawnPoint.z -= mainTileWidth;
        if (checkRandom(spawnTile))
        {
            float spawnFirstPos = getRandom(noSpawnRate);
            if (checkRandom(spawnFirstPos))
            {
                GameObject firstSpawnable = getRandomSpawnable();
                spawnPoint.x = midPoint;
                Instantiate(firstSpawnable, spawnPoint,
                    firstSpawnable.transform.rotation);
            }

            float spawnSecondPos = getRandom(noSpawnRate);
            if (checkRandom(spawnSecondPos))
            {
                GameObject secondSpawnable = getRandomSpawnable();
                spawnPoint.x = 0;
                Instantiate(secondSpawnable, spawnPoint,
                    secondSpawnable.transform.rotation);
            }

            float spawnThirdPos = getRandom(noSpawnRate);
            if (checkRandom(spawnThirdPos))
            {
                GameObject thirdSpawnable = getRandomSpawnable();
                spawnPoint.x = -midPoint;
                Instantiate(thirdSpawnable, spawnPoint,
                    thirdSpawnable.transform.rotation);
            }
        }
        StartCoroutine(spawnSpawnables());
    }

    private float getRandom(float max)
    {
        return Random.Range(0, max);
    }

    private int getRandomIndex()
    {
        float spawnRate;
        float total = 0.0f;
        for (int i = 0; i < spawnRates.Length; i++)
        {
            spawnRate = spawnRates[i];
            if (float.IsPositiveInfinity(spawnRate))
            {
                return i;
            }
            else if (spawnRate >= 0f && !float.IsNaN(spawnRate))
            {
                total += spawnRates[i];
            }
        }

            float random = Random.value;
            float check = 0f;

            for (int i = 0; i < spawnRates.Length; i++)
            {
            spawnRate = spawnRates[i];
            if (float.IsNaN(spawnRate) || spawnRate <= 0f) continue;
                check += spawnRate / total;
                if (check >= random) return i;
            }

            return -1;
        }

    private GameObject getRandomSpawnable()
    {
        int idx = Mathf.Max(0, getRandomIndex());
        return spawnables[idx];
    }

    private bool checkRandom(float check)
    {
        return (check <= 1);
    }
}

