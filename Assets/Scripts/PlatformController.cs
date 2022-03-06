using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatformController : MonoBehaviour
{
    public event Action<int> OnSizechanged = delegate { };

    [SerializeField]
    private float speed;
    [SerializeField]
    private Ball ballPrefab;
    [SerializeField]
    private Transform spawnBallPosition;

    [SerializeField]
    private GameObject[] platformSizes;

    private Ball ballOnThePlatform;
    private Rigidbody2D rigidbody2D;
    private int currentSize = 1;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        PrefabCollector<Ball>.Instance.SetSketch(ballPrefab);
        AddBall();
    }

    private void Start()
    {
        GameState.instace.OnLifeChanged += lifes =>
        {
            if(lifes>0)
                AddBall();
        };

        GameState.instace.OnStartGame += () =>
        {
            AddBall();
        };
    }

    public int GetSize()
    {
        return currentSize;
    }

    public void FreeBall()
    {
        if (ballOnThePlatform != null)
        {
            ballOnThePlatform.Throw();
            ballOnThePlatform = null;
        }
    }

    public void Move(float direction)
    {
        rigidbody2D.velocity = new Vector2(direction * speed, 0);
    }

    #region Pickups
    public void SizeUp()
    {
        if (currentSize < 3)
            currentSize++;
        SizeChanged();
    }

    public void SizeDown()
    {
        if (currentSize > 1)
            currentSize--;
        SizeChanged();
    }

    public void AddBall()
    {
        if (ballOnThePlatform == null)
        {
            ballOnThePlatform = PrefabCollector<Ball>.Instance.GetFreePrefab();
            ballOnThePlatform.transform.position = spawnBallPosition.position;
            ballOnThePlatform.transform.parent = transform;
            GameState.instace.AddBall();
        }
    }
    #endregion

    private void SizeChanged()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == currentSize - 1)
            {
                platformSizes[i].gameObject.SetActive(true);
            }
            else
            {
                platformSizes[i].gameObject.SetActive(false);
            }
        }
        OnSizechanged(currentSize);
    }
}
