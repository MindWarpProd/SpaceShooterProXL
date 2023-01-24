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
        StartCoroutine(EnemySpawner());
        StartCoroutine(PowerUpSpawner());
        
    }


    /// <summary>
    /// SpawnManager-EnemySpawner:
    /// Spawns Enemy Ships
    /// </summary>
    private IEnumerator EnemySpawner()
    {

        while (_isSpawning)
        {
            GameObject newEnemy = Instantiate(_spawnEnemy, new Vector3(Random.Range(-8, 8), 7, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnSpeed);
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
        while (_isSpawning)
        {
            yield return new WaitForSeconds(Random.Range(7f, 10f));
            Instantiate(_powerUp[Random.Range(0, _powerUp.Length)], new Vector3(Random.Range(-8, 8), 7, 0), Quaternion.identity);
        }
    }

  
}
