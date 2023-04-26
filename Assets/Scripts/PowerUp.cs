using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float _speed = 3;
    private Player _player;
    private string _powerUpHit;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
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
