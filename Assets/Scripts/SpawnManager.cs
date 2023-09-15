using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] collectItemPrefabs;
    private Vector3 spawnObstaclePos = new Vector3 (25, -1.7f, 0);
    private Vector3 spawnCollectItemPos = new Vector3 (20, 1.7f, 0);
    private float startObstacleDelay = 2;
    private float repeatObstacleRate = 2; 
    private PlayerController playerControllerScript;
    private int spawnTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startObstacleDelay, repeatObstacleRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        if ( !playerControllerScript.gameOver && !playerControllerScript.isIdling )
        {
            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            int collectItemIndex = Random.Range(0, collectItemPrefabs.Length);
            spawnTime++;
            //Instantiate(obstaclePrefabs[obstacleIndex], spawnObstaclePos, obstaclePrefabs[obstacleIndex].transform.rotation);
            if (spawnTime % 2 == 0)
            {
                Instantiate(obstaclePrefabs[0], spawnObstaclePos, obstaclePrefabs[0].transform.rotation);
                Instantiate(obstaclePrefabs[1], spawnObstaclePos + new Vector3(4, 3.7f, 0), obstaclePrefabs[0].transform.rotation);
            } else
            {
                Instantiate(obstaclePrefabs[1], spawnObstaclePos + new Vector3(0, 3.7f, 0), obstaclePrefabs[1].transform.rotation);
                Instantiate(obstaclePrefabs[0], spawnObstaclePos + new Vector3(4, 0, 0), obstaclePrefabs[0].transform.rotation);
            }

            Instantiate(collectItemPrefabs[collectItemIndex], spawnCollectItemPos, collectItemPrefabs[collectItemIndex].transform.rotation);
        }
    }
}
