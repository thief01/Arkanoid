using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private const int POINTS_FOR_HIT = 10;
    private const int POINTS_FOR_DESTROY = 100;
    private const float CHANCE_TO_DROP = 50;

    [SerializeField]
    private int health;

    [SerializeField]
    private GameObject[] pickupDrop;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        health--;
        GameState.instace.AddPoints(POINTS_FOR_HIT);
        if(health<=0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        if (pickupDrop.Length > 0)
        {
            float random = Random.RandomRange(0, 100);
            if (random < CHANCE_TO_DROP)
            {
                int id = Random.Range(0, pickupDrop.Length);
                GameObject go = Instantiate(pickupDrop[id]);
                go.transform.position = transform.position;
            }
        }
        GameState.instace.AddPoints(POINTS_FOR_DESTROY);
        Destroy(gameObject);
    }
}
