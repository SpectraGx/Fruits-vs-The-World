using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    
    private State state;
    /*
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingToStartTimer;
    */


    private bool isGamePaused = false;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject uiGame;

    /*
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private float gamePlayingToStartTimerMax = 90f;
    [SerializeField] private GameObject gameOverFirstButton;
    */


    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
        Time.timeScale = 1f;
    }

    /*
    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingToStartTimer = gamePlayingToStartTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingToStartTimer -= Time.deltaTime;
                if (gamePlayingToStartTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                //SelectPlayButton();
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(gameOverFirstButton);
                break;
        }
        Debug.Log(state);
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (gamePlayingToStartTimer / gamePlayingToStartTimerMax);
    }
    */

    public void Pause()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            uiGame.SetActive(false);
            pauseMenu.SetActive(true);
            Time.timeScale = 0.000001f;
            //musicSource.Pause();
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            uiGame.SetActive(true);
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            //musicSource.Play();
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }

    }
}
