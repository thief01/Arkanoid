using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapState : MonoBehaviour
{
    private const float X_BRICK_SIZE = 0.67f;
    private const float Y_BRICK_SIZE = 0.27f;

    private readonly int[] MAX_BRICKS_BY_ID = { 10000, 200, 50, 30, 20 };

    public static MapState instance;

    [SerializeField]
    private int xSize;
    [SerializeField]
    private int ySize;
    [SerializeField]
    private Brick brickPrefab;

    private List<Brick> spawnedBricks = new List<Brick>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            PrefabCollector<Brick>.Instance.SetSketch(brickPrefab);
            GameState.instace.OnStartGame += SpawnMap;
            SpawnMap();
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                Vector3 offset = new Vector3(j * X_BRICK_SIZE, i * Y_BRICK_SIZE, 0);
                Gizmos.DrawWireCube(offset + transform.position, new Vector3(X_BRICK_SIZE, Y_BRICK_SIZE, 1));
            }
        }
    }

    public void BrickDestroyed(Brick g)
    {
        spawnedBricks.Remove(g);

        if (spawnedBricks.Count <= 0)
        {
            SpawnMap();
        }
    }

    private void SpawnMap()
    {
        int[] currentBricks = new int[5];
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                Vector3 offset = new Vector3(j * X_BRICK_SIZE, i * Y_BRICK_SIZE, 0);
                Vector3 position = offset + transform.position;
                float brickDurability = Mathf.PerlinNoise(position.y, Random.Range(0, (position.x + position.y) * 5)) * (Brick.MAX_HEALTH + 2);
                int blockId = Mathf.RoundToInt(brickDurability);
                if (blockId < Brick.MAX_HEALTH)
                {
                    while (currentBricks[blockId] > MAX_BRICKS_BY_ID[blockId])
                    {
                        blockId--;
                    }
                    Brick brick = PrefabCollector<Brick>.Instance.GetFreePrefab();
                    brick.transform.parent = transform;
                    brick.Health = blockId + 1;
                    brick.transform.position = position;
                    currentBricks[blockId]++;
                    spawnedBricks.Add(brick);
                }
            }
        }
    }
}
