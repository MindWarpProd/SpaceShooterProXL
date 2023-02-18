using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;


public class Enemy : MonoBehaviour
{

    [SerializeField] float _enemySpeed = 4f;
    private Player _player;
    [SerializeField] bool _isPlayingAnimation;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Enemy::Player is Null");
        }
        anim = this.gameObject.GetComponent<Animator>();
        if(anim == null)
        {
            Debug.LogError("Enemy::Anim is Null");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _enemySpeed);
        if (transform.position.y <= -7f)
        {
            transform.position = new Vector3(Random.Range(-10f, 10f), 7f, transform.position.z);
        }
        if (_isPlayingAnimation)
        {
            if(this.transform.position.y <= -6)
            {
                Destroy(this.gameObject);
            }
            /*if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
            {
                Destroy(this.gameObject);
            }*/

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.tag == "Player")
        {

            if (_player != null)
            {
                if (anim != null)
                {
                    anim.SetTrigger("OnEnemyDeath");
                }
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                _player.Damage();
            }
            _isPlayingAnimation = true;
            Destroy(this.gameObject, 2.5f);
        }
        else if (other.tag == "Laser")
        {
            if (_player != null)
            {
                if (anim != null)
                {
                    anim.SetTrigger("OnEnemyDeath");
                }
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                _player.PlayerScore(10);
                Destroy(other.gameObject);
                _isPlayingAnimation = true;
                Destroy(this.gameObject, 2.5f);
            }

        }
    }
}
