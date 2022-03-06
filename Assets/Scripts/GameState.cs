using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameState : MonoBehaviour
{
    private const int MAX_BALLS_TO_CLONE = 20;
    public static GameState instace;

    public event Action OnStartGame = delegate { };
    public event Action OnCloneBalls = delegate { };
    public event Action<int> OnScoreChanged = delegate { };
    public event Action<int> OnLifeChanged = delegate { };
    public event Action OnEndGame = delegate { };

    private int score = 0;
    private int lifes = 3;
    private int countOfBalls = 0;

    private void Awake()
    {
        if (instace == null)
            instace = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        OnScoreChanged?.Invoke(score);
        OnLifeChanged?.Invoke(lifes);
    }

    public void RestartGame()
    {
        OnEndGame();
        lifes = 3;
        score = 0;
        countOfBalls = 0;
        OnScoreChanged?.Invoke(score);
        OnLifeChanged?.Invoke(lifes);
        OnStartGame?.Invoke();
    }

    public void CloneBalls()
    {
        if (countOfBalls < MAX_BALLS_TO_CLONE)
            OnCloneBalls?.Invoke();
    }

    public void AddPoints(int points)
    {
        score += points;
        OnScoreChanged(score);
    }

    public void AddBall()
    {
        countOfBalls++;
    }

    public void RemoveBall()
    {
        countOfBalls--;
        if (countOfBalls <= 0)
        {
            lifes--;
            OnLifeChanged?.Invoke(lifes);
            if (lifes <= 0)
                OnEndGame?.Invoke();
        }
    }
}
