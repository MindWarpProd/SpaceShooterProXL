using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
// will need var for the audio clips
public class AudioManagerScript : MonoBehaviour
{
    [SerializeField] AudioClip _laserFire;
    [SerializeField] AudioClip _playerHit;
    [SerializeField] AudioClip _explosion; 
    [SerializeField] AudioClip _powerUp; 

    AudioSource _audioSource;
    private void OnEnable()
    {
        Player.LaserFire += Player_LaserFire;
        Player.PlayerHit += Player_PlayerHit;
        Enemy.EnemyDestroy += Enemy_EnemyDestroy;
        PowerUp.PowerUpCollected += PowerUp_PowerUpCollected;
    }

    private void PowerUp_PowerUpCollected()
    {
        PowerUpSound();
    }

    private void Enemy_EnemyDestroy()
    {
        Explosion();
    }

    private void Player_PlayerHit()
    {
        LaserHit();
    }

    private void Player_LaserFire()
    {
        LaserShot();
        
    }

    // will start action events to listen for fire, explosion, and power up
    //these will start the play of the sounds
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
            Debug.LogError("AudioManagerScript::_audioSource is Null");

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LaserHit()
    {
        _audioSource?.PlayOneShot(_playerHit);
    }
    public void LaserShot()
    {
        _audioSource?.PlayOneShot(_laserFire);
    }
    public void Explosion()
    {
        _audioSource?.PlayOneShot(_explosion);
    }
    public void PowerUpSound()
    {
        _audioSource?.PlayOneShot(_powerUp);
    }
    private void OnDisable()
    {
        Player.LaserFire -= Player_LaserFire;
        Player.PlayerHit -= Player_PlayerHit;
    }
}
