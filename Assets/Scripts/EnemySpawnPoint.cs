using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public Color gizmoColor = Color.white;
    public Vector3 boxSize = new Vector3(1, 2, 1);

    public GameObject enemyPrefab;

    public float nextSpawn = 0;
    public float spawnRate;
    public float maxEnemies;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if the max enemies is higher than the amount spawned
        if(GameManager.Instance.enemies.Count < maxEnemies)
        {
            // Checks if it needs to spawn
            if (Time.time > nextSpawn)
            {
                // Sets the spawn to the correct time
                nextSpawn = Time.time + spawnRate;
                // Function
                SpawnEnemy();
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Gizmo Color
        Gizmos.color = gizmoColor;
        
        // Sets an offset for the box
        float boxOffsetY = boxSize.y / 2;

        // Draws cube for refrence of placment and ray for rotation
        Gizmos.DrawCube(transform.position + (boxOffsetY * Vector3.up), new Vector3(1,2,1));
        Gizmos.DrawRay(transform.position + (boxOffsetY * Vector3.up), transform.forward);
    }

    public void SpawnEnemy()
    {
        // Instantiates the enemy
        GameObject newEnemy = (GameObject)Instantiate(enemyPrefab, transform.position, transform.rotation);
        // adds that enemy to the list of enemies
        GameManager.Instance.enemies.Add(newEnemy);
    }
}
