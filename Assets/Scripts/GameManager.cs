using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver;
    public static event Action<bool> GameOverEvent;
    // Start is called before the first frame update
    void Start()
    {
        _isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene("MainMenu");
        }

    }
    /// <summary>
    /// Sets game over in Game Manager
    /// </summary>
    public void GameOver()
    {
        _isGameOver = true;
        GameOverEvent?.Invoke(_isGameOver);
    }
}

[System.Serializable]
public class UnityFloatEvent : UnityEvent<float> { }
[System.Serializable]
public class UnityIntEvent : UnityEvent<int> { }
[System.Serializable]
public class UnityBoolEvent : UnityEvent<bool> { }


