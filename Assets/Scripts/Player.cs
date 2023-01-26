using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("Add Score from UI")]
    public UnityFloatEvent OnScoreChange;
    [Header("Add Lives from UI")]
    public UnityIntEvent OnLivesChange;
    [Header("No lives Left")]
    public UnityEvent OnPlayerDeath;
    [SerializeField] float _speed = 3.5f;
    [SerializeField] float _speedRegular = 3.5f;
    [SerializeField] float _speedBoost = 6f;
    [SerializeField] GameObject _laser;
    [SerializeField] GameObject _tripleLaser;
    [SerializeField] float _setCoolDown = 4f;
    [SerializeField] float _coolDownTimer;
    [SerializeField] int _lives = 3;
    [SerializeField] float _activePowerUp = 100f;
    [SerializeField] bool _boostEnabled = false;
    [SerializeField] bool _shieldEnabled = false;
    private SpawnManager _spawnManager;
    [SerializeField] string _attackType = "SingleLaser";
    [SerializeField] float _score;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Player::spawnManager is null");
        }
        // take the current pos = new pos (0,0,0)
        this.transform.position = Vector3.zero;
        _score = 0;


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
        if (_boostEnabled)
        {
            _speed = _speedBoost;
            StartCoroutine(SpeedBoostTime(_activePowerUp));
            
        }
        else _speed = _speedRegular;
        transform.Translate(direction * _speed * Time.deltaTime);
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
       /* if (_coolDownTimer >= 0)
        {
            //cool down active
            _coolDownTimer -= Time.deltaTime;
        }
        else
        { */

            if (Input.GetKeyDown(KeyCode.Space))
            {
                switch (_attackType)
                {
                    case "SingleLaser":
                        Instantiate(_laser, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
                        _coolDownTimer = _setCoolDown;
                        break;
                    case "TripleLaser":
                        Instantiate(_tripleLaser, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
                        _coolDownTimer = _setCoolDown;
                        break;
                    default:
                        Debug.Log("Player::No laser selected");
                        break;
                }
            }
       // }

    }
    /// <summary>
    /// Player Damage()
    /// Damages player object lives
    /// </summary>
    public void Damage()
    {

        if (!_shieldEnabled)
        {
            _lives--;
            OnLivesChange.Invoke(_lives);

            if (_lives <= 0)
            {
                OnPlayerDeath.Invoke();
                if (_spawnManager != null)
                {
                    _spawnManager.StopEnemySpawn();
                }
                Destroy(this.gameObject);
            }
        }
        else
        {
            _shieldEnabled = false;
            this.transform.GetChild(0).gameObject.SetActive(false);
        }

    }
    /// <summary>
    /// TripleShot()
    /// Activates the Triple Shot PowerUp
    /// </summary>
    public void TripleShot()
    {
        _attackType = "TripleLaser";
        StartCoroutine(PowerAttackTime(_activePowerUp));

    }
    /// <summary>
    /// PowerUpCoolDown(float)
    /// The power up cool down method
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator PowerAttackTime(float time)
    {
        Debug.Log("in attach timer");
        yield return new WaitForSeconds(time);

        _attackType = "SingleLaser";

    }
    /// <summary>
    /// Speed boost time down method
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator SpeedBoostTime(float time)
    {
        Debug.Log("in speed timer");
        yield return new WaitForSeconds(time);
        _boostEnabled = false;
    }
    /// <summary>
    /// Speed powerup activate
    /// </summary>
    public void SetSpeedPowerUp()
    {
        _boostEnabled = true;
    }
    /// <summary>
    /// Shield powerup activate
    /// </summary>

    public void SetShieldPowerUp()
    {
        _shieldEnabled = true;
        this.transform.GetChild(0).gameObject.SetActive(true);
    }
    /// <summary>
    /// Adjust score and Invoke UI
    /// </summary>
    /// <param name="score"></param>
    public void PlayerScore(float score)
    {
        _score += score;

        OnScoreChange.Invoke(_score);

    }


}
