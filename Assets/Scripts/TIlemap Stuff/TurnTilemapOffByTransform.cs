using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTilemapOffByTransform : MonoBehaviour
{
    private ObjectPoolNS tilemap_pool;
    // Start is called before the first frame update
    void Start()
    {
        tilemap_pool = GetComponentInParent<ObjectPoolNS>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= GameManager.manager.turn_tilemap_off_x)
        {
            if (GameManager.manager.bossBattle)
            {
                if (!GameManager.manager.spawned_checkpoint)
                {
                    tilemap_pool.SpawnCheckpointTilemap();
                    GameManager.manager.spawned_checkpoint = true;
                }
                else
                {
                    tilemap_pool.SpawnBossTM();
                }
            }
            else
            {
                tilemap_pool.SpawnTileMap();
            }
            gameObject.SetActive(false);
        }
    }
}
