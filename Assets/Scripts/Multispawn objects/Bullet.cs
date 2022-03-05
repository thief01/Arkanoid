using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 15;

    private void Awake()
    {
        
        PrefabCollector<Bullet>.Instance.Destroy(this, 10);
    }

    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1).normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PrefabCollector<Bullet>.Instance.Destroy(this);
    }
}
