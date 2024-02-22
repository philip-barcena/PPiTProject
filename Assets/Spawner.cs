using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject[] obstaclePrefabs;
    public float obstacleSpawnTimer = 2f;
    public float obstacleVelocity = 1f;

    private float timeUntilObsSpawn;

    private void Update() {
        if (GameManager.Instance.isPlaying) {
            SpawnLoop();
        }
    }
    private void SpawnLoop() {
        timeUntilObsSpawn += Time.deltaTime;

        if(timeUntilObsSpawn >= obstacleSpawnTimer){
            Spawn();
            timeUntilObsSpawn = 0f;
        }
    }

    private void Spawn() {
        GameObject obstacleSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        GameObject spawnedObstacle = Instantiate(obstacleSpawn, transform.position, Quaternion.identity);

        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
        obstacleRB.velocity = Vector2.left * obstacleVelocity;
    }



}
