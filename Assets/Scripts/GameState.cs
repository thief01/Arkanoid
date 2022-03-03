using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameState : MonoBehaviour
{
    private const int MAX_BALLS_TO_CLONE = 20;
    public static GameState instace;

    public event Action OnCloneBalls = delegate { };
    public event Action<int> OnScoreChanged = delegate { };
    public event Action<int> OnLifeChanged = delegate { };

    private int score=0;
    private int lifes = 3;
    private int countOfBalls = 0;

    private void Awake()
    {
        if (instace == null)
            instace = this;
    }

    private void Start()
    {
        OnScoreChanged?.Invoke(score);
        OnLifeChanged?.Invoke(lifes);
    }

    public void CloneBalls()
    {
        if(countOfBalls < MAX_BALLS_TO_CLONE)
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
        if(countOfBalls<=0)
        {
            lifes--;
            OnLifeChanged(lifes);
        }
    }
}
