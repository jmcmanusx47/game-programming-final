using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public Transform mainTileObj;
    public int spawnSpeed = 1;
    public int mainTileWidth = 4;
    public int startingTiles = 5;

    private Vector3 nextTileSpawn;

    // Start is called before the first frame update
    void Start()
    {
        nextTileSpawn.z = mainTileWidth * startingTiles;
        StartCoroutine(spawnTile());
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
        Instantiate(mainTileObj, nextTileSpawn, mainTileObj.rotation);
        nextTileSpawn.z += mainTileWidth;
        StartCoroutine(spawnTile());
    }
}

