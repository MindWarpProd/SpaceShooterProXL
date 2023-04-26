using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _spawnEnemy;
    [SerializeField] GameObject _enemyContainer;
    [SerializeField] GameObject[] _powerUp;
    [SerializeField] float _spawnSpeed = 5;
    [SerializeField] bool _isSpawning = true;
   
    // Start is called before the first frame update
    void Start()
    {


    }
    /// <summary>
    /// Starts the spawning of the enemy and PowerUps
    /// </summary>
    public void StartSpawning()
    {
        StartCoroutine(PowerUpSpawner());
        StartCoroutine(EnemySpawner());
       
    }




    /// <summary>
    /// SpawnManager-EnemySpawner:
    /// Spawns Enemy Ships
    /// </summary>
    private IEnumerator EnemySpawner()
    {
        while (_isSpawning)
        {
            yield return new WaitForSeconds(_spawnSpeed);
            GameObject newEnemy = Instantiate(_spawnEnemy, new Vector3(Random.Range(-8, 8), 7, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
        }
    }
    /// <summary>
    /// SpawnManager-StopEnemySpawn:
    /// Stops Enemy from Spawning
    /// </summary>
    public void StopEnemySpawn()
    {
        _isSpawning = false;
    }


    /// <summary>
    /// PowerUpSpawner()
    /// Creates Power Ups
    /// </summary>
    private IEnumerator PowerUpSpawner()
    {
        Debug.Log("In powerup");
        while (_isSpawning)
        {
            Debug.Log("Before PU Spawnign");
            yield return new WaitForSeconds(Random.Range(7f, 10f));
            Debug.Log("After PU Spawn");
            Instantiate(_powerUp[Random.Range(0, _powerUp.Length)], new Vector3(Random.Range(-8, 8), 7, 0), Quaternion.identity);
        }
    }


}
