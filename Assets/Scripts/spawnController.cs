using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyPrefabs;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject[] asteroids;    

    void Start()
    {               
            StartCoroutine(EnemySpawnRoutine());
            StartCoroutine(PowerSpawnRoutine());
            StartCoroutine(AsteroidsSpawnRoutine());       
    }
    
    IEnumerator EnemySpawnRoutine()
    {
        while (true)
        {
            int randEnemy = Random.Range(0, 5);
            Instantiate(enemyPrefabs[randEnemy], new Vector3(Random.Range(-2.3f, 2.3f), 5.7f, 0), Quaternion.Euler(0, 0, 180));
            yield return new WaitForSeconds(1.4f);

        }
    }
    IEnumerator PowerSpawnRoutine()
    {
        while (true)
        {
            int randpower = Random.Range(0, 3);
            Instantiate(powerups[randpower], new Vector3(Random.Range(-1.3f, 1.3f), 7f, 0), Quaternion.identity);
            yield return new WaitForSeconds(10f);

        }
    }
    IEnumerator AsteroidsSpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(13f);
            int randasteroids = Random.Range(0, 3);
            Instantiate(asteroids[randasteroids], new Vector3(Random.Range(-1.5f, 1.5f), 6.5f, 0), Quaternion.identity);            

        }
    }
    
}
