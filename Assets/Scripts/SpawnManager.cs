using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject spawnEnemy;
    public GameObject enemyContainer;
    [SerializeField] float _spawnSpeed = 5;
    [SerializeField] bool _isSpawning = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawner());
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// SpawnManager-EnemySpawner:
    /// Spawns Enemy Ships
    /// </summary>
    private IEnumerator EnemySpawner()
    {

        while (_isSpawning)
        {
            GameObject newEnemy = Instantiate(spawnEnemy, new Vector3(Random.Range(-11, 11), 7, 0), Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
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


}
