using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _GameOver;
    [SerializeField] Sprite[] _liveSprites;
    public UnityEngine.UI.Image _liveImage;

    // Start is called before the first frame update
    void Start()
    {
        if(_scoreText != null)     
        _scoreText.text = "Score: " + 0;
        if(_liveImage != null)
        _liveImage.sprite = _liveSprites[3];

    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Update the score
    /// </summary>
    /// <param name="score"></param>
    public void UpdateScore(float score)
    {
        if (_scoreText != null)
        {
            _scoreText.text = "Score: " + score;
        }
    }
    /// <summary>
    /// Update lives of Player and call game over
    /// </summary>
    /// <param name="lives"></param>
    public void UpdateLives(int lives)
    {
        if (_liveImage != null)
        {
            _liveImage.sprite = _liveSprites[lives];
            if (lives <= 0)
            {
                _GameOver.gameObject.SetActive(true);
            }
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }



}
