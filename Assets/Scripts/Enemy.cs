using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{

    [SerializeField] float _enemySpeed = 4f;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
       _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _enemySpeed);
        if (transform.position.y <= -7f)
        {
            transform.position = new Vector3(Random.Range(-10f, 10f), 7f, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.tag == "Player")
        {
            
            if (_player != null)
            {
                _player.Damage();
            }
            Destroy(this.gameObject);
        }
        else if (other.tag == "Laser")
        {
            _player.PlayerScore(10);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
