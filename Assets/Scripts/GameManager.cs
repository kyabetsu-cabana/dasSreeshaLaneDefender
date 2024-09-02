using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Variables for enemies in the world
    public List<GameObject> Enemies;
    public List<GameObject> EnemySpawnLanes;
    [SerializeField] private float spawnTimer;
    [SerializeField] private float spawnTimerMax;

    // Update is called once per frame
    void Update()
    {
        // Starts counting up to spawnTimerMax
        spawnTimer += Time.deltaTime;

        // Checks to see if the spawn timer has reached the maximum limit
        if (spawnTimer > spawnTimerMax)
        {
            // Resets the timer to 0
            spawnTimer = 0;

            // Takes a random enemy from the list
            int enemyType = Random.Range(0, Enemies.Count);

            // Takes a random lane from the list to spawn the enemy on
            int LaneNum = Random.Range(0, EnemySpawnLanes.Count);

            // Spawns a random enemy on a random lane
            Instantiate(Enemies[enemyType], EnemySpawnLanes[LaneNum].transform.position, Quaternion.identity);
        }
    }
}
