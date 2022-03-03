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
    private GameObject ball;
    [SerializeField]
    private Transform spawnBallPosition;

    [SerializeField]
    private GameObject[] platformSizes;

    private GameObject ballOnThePlatform;
    private Rigidbody2D rigidbody2D;
    private int currentSize = 1;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        AddBall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetSize()
    {
        return currentSize;
    }

    public void FreeBall()
    {
        if(ballOnThePlatform!=null)
            ballOnThePlatform.GetComponent<Ball>().Throw();
    }

    public void Move(float direction)
    {
        rigidbody2D.velocity = new Vector2(direction* speed, 0);
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
        if(ballOnThePlatform == null)
        {
            ballOnThePlatform = Instantiate(ball, transform);
            ballOnThePlatform.transform.position = spawnBallPosition.position;
        }
    }
    #endregion

    private void SizeChanged()
    {
        for(int i=0; i<3; i++)
        {
            if(i==currentSize-1)
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
