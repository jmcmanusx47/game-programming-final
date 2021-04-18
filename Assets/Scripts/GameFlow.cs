using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    public Transform mainTileObj;
    public int spawnSpeed = 1;
    public int mainTileWidth = 4;
    public int treeWidth = 17;
    public int mainTileLength = 12;
    public int startingTiles = 5;
    public GameObject[] spawnables;
    public float spawnTileRate = 1.75f;
    public float[] spawnRates;
    public float noSpawnRate = 1.75f;
    public float spawnEnemiesY = 1.5f;
    public GameObject[] scenery;
    public GameObject ground;
    public float groundSize;

    public GameObject bossRoom;
    public float distanceToBoss = 20;
    public int levelRequiredForBoss = 1;
    private bool bossSpawned;


    private Vector3 nextGroundSpawn;
    private Vector3 nextTileSpawn;
    private Vector3 nextTreeSpawnLeft;
    private Vector3 nextTreeSpawnRight;
    private Vector3 spawnPoint;
    private float midPoint;
    int totalTiles;

    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Make sure spawnables and spawnrates are of same size.");
        nextGroundSpawn.z = groundSize;
        nextTileSpawn.z = mainTileWidth * startingTiles;
        nextTreeSpawnLeft.z = treeWidth;
        nextTreeSpawnLeft.x = -17;
        nextTreeSpawnRight.z = treeWidth;
        nextTreeSpawnRight.x = 17;
        spawnPoint = nextTileSpawn;
        midPoint = mainTileLength / 4;
        totalTiles = 5;
        StartCoroutine(spawnTile());
        StartCoroutine(spawnSpawnables());
        StartCoroutine(spawnScenery());
        StartCoroutine(spawnGround());

        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (!bossSpawned && player.transform.position.z >= distanceToBoss && player.GetComponent<PlayerLevel>().level >= levelRequiredForBoss)
        {
            spawnBossRoom();
        }
    }

    IEnumerator spawnScenery()
    {
        yield return new WaitForSeconds(2 * spawnSpeed);

        int randomIndexL = Random.Range(0, scenery.Length);
        int randomIndexR = Random.Range(0, scenery.Length);
        GameObject sceneObjectL = scenery[randomIndexL];
        GameObject sceneObjectR = scenery[randomIndexR];

        Instantiate(sceneObjectL, nextTreeSpawnLeft, mainTileObj.rotation);
        Instantiate(sceneObjectR, nextTreeSpawnRight, mainTileObj.rotation);
        nextTreeSpawnLeft.z += treeWidth;
        nextTreeSpawnRight.z += treeWidth;
        
        StartCoroutine(spawnScenery());
    }

    IEnumerator spawnGround()
    {
        yield return new WaitForSeconds(spawnSpeed * 5);
        Instantiate(ground, nextGroundSpawn, mainTileObj.rotation);
        nextGroundSpawn.z += groundSize;
        StartCoroutine(spawnGround());
    }

    IEnumerator spawnTile()
    {
        yield return new WaitForSeconds(spawnSpeed);
        Instantiate(mainTileObj, nextTileSpawn, mainTileObj.rotation);
        nextTileSpawn.z += mainTileWidth;
        totalTiles++;
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

    private void spawnBossRoom()
    {
        StopAllCoroutines();

        int currentScene = GlobalControl.Instance.currentSceneIndex;
        print(currentScene);
        //level 2 boss room offset: (-.53f, 9.6f, 2f)
        //level 1 boss room offset (-1.92, 3.767, -1f)
        if (currentScene == 2)
        {
            Instantiate(bossRoom, nextTileSpawn - new Vector3(-1.92f, 3.767f, -1f), mainTileObj.rotation);
        }
        else if (currentScene == 3)
        {
            Instantiate(bossRoom, nextTileSpawn + new Vector3(-.53f, 9.6f, 2f), mainTileObj.rotation);
        }
        

        bossSpawned = true;
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

