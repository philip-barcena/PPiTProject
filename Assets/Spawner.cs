using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private Transform obstacleParent;
    public float obstacleSpawnTimer = 2f;

    [Range(0, 1)] public float obstacleSpawnTimeFactor = 0.1f;
    public float obstacleVelocity = 1f;

    [Range(0, 1)] public float obstacleVelocityFactor = 0.2f;

    private float _obstacleSpawnTimer;
    private float _obstacleVelocity;

    private float timeAlive;
    private float timeUntilObsSpawn;

    private void Start() {
        GameManager.Instance.onGameOver.AddListener(ClearObstacles);
        GameManager.Instance.onPlay.AddListener(ResetTimers);
        
    }

    private void Update() {
        if (GameManager.Instance.isPlaying) {
            timeAlive += Time.deltaTime;

        CalculateFactors();

            SpawnLoop();
        }
    }
    private void SpawnLoop() {
        timeUntilObsSpawn += Time.deltaTime;

        if(timeUntilObsSpawn >= _obstacleSpawnTimer){
            Spawn();
            timeUntilObsSpawn = 0f;
        }
    }

    private void ClearObstacles() {
        foreach(Transform child in obstacleParent) {
            Destroy(child.gameObject);
        }
    }

    private void CalculateFactors() {
        _obstacleSpawnTimer = obstacleSpawnTimer / Mathf.Pow(timeAlive, obstacleSpawnTimeFactor);
        _obstacleVelocity = obstacleVelocity * Mathf.Pow(timeAlive, obstacleVelocityFactor);
    }

    private void ResetTimers () {
        timeAlive = 1f;
        _obstacleSpawnTimer = obstacleSpawnTimer;
        _obstacleVelocity = obstacleVelocity;
    }

    private void Spawn() {
        GameObject obstacleSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        GameObject spawnedObstacle = Instantiate(obstacleSpawn, transform.position, Quaternion.identity);
        spawnedObstacle.transform.parent = obstacleParent;

        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
        obstacleRB.velocity = Vector2.left * _obstacleVelocity;
    }



}
