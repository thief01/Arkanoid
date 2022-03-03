using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private const int POINTS_FOR_HIT = 10;
    private const int POINTS_FOR_DESTROY = 100;

    [SerializeField]
    private int health;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        health--;
        GameState.instace.AddPoints(POINTS_FOR_HIT);
        if(health<=0)
        {
            GameState.instace.AddPoints(POINTS_FOR_DESTROY);
            Destroy(gameObject);
        }
    }
}
