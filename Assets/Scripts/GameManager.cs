using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public event EventHandler OnStateChange;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    public enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 12f;
    private bool IsGamePaused;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
        OnStateChange?.Invoke(this, EventArgs.Empty);
    }

    private void Start()
    {
        GameInputs.Instance.OnPauseAction += GameInputs_OnPauseAction;
    }

    private void GameInputs_OnPauseAction(object sender, EventArgs e)
    {
        ToggleGamePause();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f)
                {
                    state = State.CountdownToStart;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GameOver:
                OnStateChange?.Invoke(this, EventArgs.Empty);
                break;
        }

        //Debug.Log(state);
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;   
    }
    public bool IsCountDownToStartActive()
    {
        return state == State.CountdownToStart;   
    }
    public bool IsGameOverState()
    {
        return state == State.GameOver;   
    }

    public float GetCountDownStartTimer()
    {
        return countdownToStartTimer;
    }
   
    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void ToggleGamePause() 
    {
        IsGamePaused = !IsGamePaused;
        if (IsGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }
}