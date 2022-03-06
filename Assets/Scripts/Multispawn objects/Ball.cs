using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ball : MonoBehaviour
{
    private const float BALL_SIZE = 0.135f;
    private const float PLATFORM_SIZE_SCALE = 0.35f;
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private LayerMask layer;
    private Rigidbody2D rigidbody2D;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameState.instace.OnCloneBalls += Clone;
        GameState.instace.OnEndGame += () =>
        {
            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        };
    }

    private RaycastHit2D lastRay;
    private Vector2 lastVelocity;

    void FixedUpdate()
    {
        CastRay();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "deathZone")
        {
            GameState.instace.RemoveBall();
            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            PrefabCollector<Ball>.Instance.Destroy(this);
        }

        if (collision.gameObject.tag == "Player")
        {
            PlatformController pc = collision.gameObject.GetComponent<PlatformController>();
            if (pc != null)
            {
                float x = (transform.position.x - pc.transform.position.x) / (pc.GetSize() * PLATFORM_SIZE_SCALE);
                float y = transform.position.y - pc.transform.position.y;
                rigidbody2D.velocity = new Vector2(x, y*pc.GetSize()*speed).normalized * speed;
            }
        }

        if (rigidbody2D.velocity.magnitude < speed)
        {
            rigidbody2D.velocity = rigidbody2D.velocity.normalized * speed;
        }

        if (gameObject.activeSelf)
            StartCoroutine(DelayChangeVelocity(collision));
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        rigidbody2D.velocity = rigidbody2D.velocity.normalized * speed;
    }

    public void Throw()
    {
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        transform.parent = null;
        rigidbody2D.velocity = new Vector2(Random.Range(-1, 2), 1).normalized * speed;
    }

    public void ThrowInDirection(Vector3 direction)
    {
        rigidbody2D.velocity = direction.normalized * speed;
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }

    public void Clone()
    {
        if (rigidbody2D != null && gameObject.activeSelf)
        {
            if (rigidbody2D.bodyType != RigidbodyType2D.Kinematic)
            {
                Ball g = PrefabCollector<Ball>.Instance.GetFreePrefab();
                g.transform.position = transform.position;
                g.ThrowInDirection(-rigidbody2D.velocity);
                GameState.instace.AddBall();
            }
        }
    }

    private IEnumerator DelayChangeVelocity(Collision2D collision)
    {
        yield return new WaitForSeconds(0.030f);
        if (lastRay.collider != null && collision.gameObject.layer == LayerMask.NameToLayer("Brick"))
        {
            Vector2 v = lastRay.normal.normalized;
            v.x = v.x == 0 ? lastVelocity.x : -lastVelocity.x;
            v.y = v.y == 0 ? lastVelocity.y : -lastVelocity.y;
            rigidbody2D.velocity = v.normalized * speed;
        }
        lastRay = new RaycastHit2D();
    }

    private void CastRay()
    {
        RaycastHit2D templaterRay = Physics2D.CircleCast(transform.position, BALL_SIZE / 2, rigidbody2D.velocity, 1, layer);
        if (templaterRay.collider != null && Vector3.Distance(transform.position, templaterRay.point) > BALL_SIZE/2)
        {
            lastVelocity = rigidbody2D.velocity.normalized;
            lastRay = templaterRay;
        }
    }
}
