using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 3.5f;
    [SerializeField] GameObject _laser;
    [SerializeField] GameObject _spawnManager;
    [SerializeField] float _setCoolDown = 4f;
    [SerializeField] float _coolDownTimer;
    [SerializeField] int _lives = 3;
    private SpawnManager spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager =_spawnManager.GetComponent<SpawnManager>();
        if(spawnManager == null)
        {
            Debug.LogError("Player::spawnManager is null");
        }
       // take the current pos = new pos (0,0,0)
        this.transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        CalculateMovement();
        ShootLaser();

    }
    /// <summary>
    /// Player CalculateMovement
    /// Moves player
    /// </summary>

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput);
        transform.Translate(direction * speed * Time.deltaTime);
        if (transform.position.y <= -4 || transform.position.y >= 0)
        {
            float positionY;
            positionY = transform.position.y;
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(positionY, -5, 0));
        }

        if (transform.position.x <= -11)
        {
            transform.position = new Vector3(11, transform.position.y);
        }
        else if (transform.position.x >= 11)
        {
            transform.position = new Vector3(-11, transform.position.y);
        }
    }
    /// <summary>
    /// Player ShootLaser()
    /// Shoots the laser
    /// </summary>

    void ShootLaser()
    {
        if (_coolDownTimer >= 0)
        {
            //cool down active
            _coolDownTimer -= Time.deltaTime;
        }
        else
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(_laser, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
                _coolDownTimer = _setCoolDown;
            }
        }

    }
    /// <summary>
    /// Player Damage()
    /// Damages player object lives
    /// </summary>
    public void Damage()
    {
        _lives--;

        if (_lives <= 0)
        {
            if (spawnManager != null)
            {
                spawnManager.StopEnemySpawn();
            }
            Destroy(this.gameObject);
        }
    }
}
