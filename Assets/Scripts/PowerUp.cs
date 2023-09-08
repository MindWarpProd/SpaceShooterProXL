using UnityEngine;
using System;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float _speed = 3;
    private Player _player;
    private string _powerUpHit;
    private bool _gameOver;

    public static event Action PowerUpCollected;
    // Start is called before the first frame update

    private void OnEnable()
    {
        GameManager.GameOverEvent += GameManager_GameOverEvent;
    }

    private void GameManager_GameOverEvent(bool obj)
    {
        _gameOver = true;
    }

    void Start()
    {
        if (!_gameOver)
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
        }
        if (_player == null)
        {
            Debug.LogError("PowerUp::Player is Null");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < -7) Destroy(gameObject);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        _powerUpHit = this.gameObject.tag;
        if (_player != null)
        {
            if (collision.tag == "Player")
            {
                PowerUpCollected?.Invoke();
                switch (_powerUpHit)
                {
                    case "TripleShot":
                        _player.TripleShot();
                        break;
                    case "SpeedPowerUp":
                        _player.SetSpeedPowerUp();
                        break;
                    case "ShieldPowerUp":
                        _player.SetShieldPowerUp();
                        break;
                    default:
                        Debug.Log("Collided with unkonwn object");
                        break;
                }
                Destroy(gameObject);
            }
        }
       
    }
}
