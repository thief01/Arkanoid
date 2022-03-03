using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapState : MonoBehaviour
{
    private const float X_BRICK_SIZE = 0.67f;
    private const float Y_BRICK_SIZE = 0.27f;

    private readonly int[] MAX_BRICKS_BY_ID = { 10000, 200,50, 30,20 };

    public static MapState instance;

    [SerializeField]
    private int xSize;
    [SerializeField]
    private int ySize;

    [SerializeField]
    private GameObject[] bricksPrefabs;

    private int spawnedBricks=0;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        SpawnMap();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        for(int i=0; i<ySize; i++)
        {
            for(int j=0; j<xSize; j++)
            {
                Vector3 offset = new Vector3(j*X_BRICK_SIZE, i*Y_BRICK_SIZE, 0);
                Gizmos.DrawWireCube(offset+transform.position, new Vector3(X_BRICK_SIZE, Y_BRICK_SIZE, 1));
            }
        }
        
    }

    public void BrickDestroyed()
    {
        spawnedBricks--;
        if(spawnedBricks<=0)
        {
            SpawnMap();
        }
    }

    private void SpawnMap()
    {
        int[] currentBricks = new int[5];
        for(int i=0; i<ySize; i++)
        {
            for(int j=0; j<xSize; j++)
            {
                Vector3 offset = new Vector3(j * X_BRICK_SIZE, i * Y_BRICK_SIZE, 0);
                Vector3 position = offset + transform.position;
                float brickDurability = Mathf.PerlinNoise(position.y , position.x) * (bricksPrefabs.Length+1);
                int blockId = Mathf.RoundToInt(brickDurability);
                if(blockId < bricksPrefabs.Length)
                {
                    while(currentBricks[blockId] > MAX_BRICKS_BY_ID[blockId])
                    {
                        blockId--;
                    }
                    GameObject brick = Instantiate(bricksPrefabs[blockId], transform);
                    brick.transform.position = position;
                    spawnedBricks++;
                    currentBricks[blockId]++;
                }
            }
        }
    }
}
