using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 15;

    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D= GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rigidbody2D.velocity = new Vector2(0, 1).normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PrefabCollector<Bullet>.Instance.Destroy(this);
    }
}
