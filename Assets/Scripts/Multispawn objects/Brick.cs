using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public const int MAX_HEALTH = 5;

    private const int POINTS_FOR_HIT = 10;
    private const int POINTS_FOR_DESTROY = 100;
    private const float CHANCE_TO_DROP = 30;

    public int Health
    {
        get
        {
            return health;
        }

        set 
        {
            health = value;
            OnHealthChanged();
        }
    }

    private int health;
    [SerializeField]
    private FXExplode brickExplode;
    [SerializeField]
    private GameObject[] pickupDrop;

    [SerializeField]
    private Sprite[] spritesByHealth;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        PrefabCollector<FXExplode>.Instance.SetSketch(brickExplode);
        spriteRenderer = GetComponent<SpriteRenderer>();
        OnHealthChanged();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health--;
        GameState.instace.AddPoints(POINTS_FOR_HIT);
        if (health <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        if (pickupDrop.Length > 0)
        {
            float random = Random.Range(0, 100);
            if (random < CHANCE_TO_DROP)
            {
                int id = Random.Range(0, pickupDrop.Length);
                GameObject go = Instantiate(pickupDrop[id]);
                go.transform.position = transform.position;
            }
        }
        SpawnFX();
        GameState.instace.AddPoints(POINTS_FOR_DESTROY);
        MapState.instance.BrickDestroyed(this);
        PrefabCollector<Brick>.Instance.Destroy(this);
    }

    private void SpawnFX()
    {
        FXExplode g = PrefabCollector<FXExplode>.Instance.GetFreePrefab();
        g.transform.position = transform.position;
        g.Play();
    }

    private void OnHealthChanged()
    {
        int spriteId = health - 1;
        if(spriteId> -1 && spriteId < spritesByHealth.Length)
        {
            spriteRenderer.sprite = spritesByHealth[spriteId];
        }
    }
}
