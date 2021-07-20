using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float minY = -4.3f, maxY = 4.3f;
        [SerializeField] GameObject[] asteroidPrefabs;
        [SerializeField] GameObject enemyPrefab;
        [SerializeField] private float timer = 2f;

        void Start()
        {
            Invoke("SpawnEnemies", timer);
        }

        void SpawnEnemies()
        {
            float posY = Random.Range(minY, maxY);
            Vector3 position = transform.position;
            position.y = posY;

            if (Random.Range(0, 2) > 0)
            {
                Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)], position, Quaternion.identity);
            }
            else
            {
                Instantiate(enemyPrefab, position, Quaternion.Euler(0f, 0f, 90f));
            }
            Invoke("SpawnEnemies", timer);
        }
    }
}