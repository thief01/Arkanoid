using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float speed = 10;
    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "deathZone")
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Player")
        {
            PlatformController pc = collision.gameObject.GetComponent<PlatformController>();
            if (pc != null)
            {
                float x = (transform.position.x - pc.transform.position.x) / (pc.GetSize() * 0.35f);
                rigidbody2D.velocity = new Vector2(x, 1).normalized * speed;
            }
        }
    }

    public void Throw()
    {
        rigidbody2D.velocity = new Vector2(Random.Range(-1, 1), 1).normalized*speed;
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        transform.parent = null;
    }

    public void Clone()
    {
        GameObject g = Instantiate(gameObject);
        g.GetComponent<Rigidbody2D>().velocity = -rigidbody2D.velocity;
    }
}
