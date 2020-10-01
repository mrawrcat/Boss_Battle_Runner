using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolNS : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }


    

    public Transform Grid;
    public float speed;
    public float tileheight;
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    [Header("Spawner tags")]
    public string[] tags;
    private void Start()
    {
        
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(Grid.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);

            }
            poolDictionary.Add(pool.tag, objectPool);
        }

        //Debug.Log("you have " + Grid.childCount + " Tilemaps");
    }

    public GameObject SpawnFromPool(string tag, Vector2 pos)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with Tag " + tag + " doesnt exist");
            return null;
        }

        GameObject objToSpawn =  poolDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = pos + new Vector2(0, 0);
        //float additonal_height = objToSpawn.GetComponent<TilemapHeight>().Height;
        //tileheight += additonal_height;

        //objToSpawn.transform.SetParent(Grid.transform);

        poolDictionary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }

    public GameObject SpawnProjectile(string tag, Vector2 pos, Vector3 dir)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with Tag " + tag + " doesnt exist");
            return null;
        }

        GameObject objToSpawn = poolDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = pos;
        objToSpawn.GetComponent<Player_Projectile>().dir = dir;
        objToSpawn.GetComponent<Player_Projectile>().speed = speed;
        //objToSpawn.transform.position += dir * speed * Time.deltaTime;
        //objToSpawn.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //objToSpawn.GetComponent<Rigidbody2D>().AddForce(dir * speed, ForceMode2D.Impulse);
        poolDictionary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }

    public GameObject SpawnProjectile2(string tag, Transform pos)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with Tag " + tag + " doesnt exist");
            return null;
        }

        GameObject objToSpawn = poolDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = pos.position;

        Rigidbody2D proj_rb = objToSpawn.GetComponent<Rigidbody2D>();
        proj_rb.velocity = Vector2.zero;
        proj_rb.AddForce(pos.right * speed, ForceMode2D.Impulse);
        //objToSpawn.GetComponent<Rigidbody2D>().AddForce(pos.up * speed, ForceMode2D.Impulse);
        poolDictionary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }

    public GameObject SpawnParticle(string tag, Vector2 pos)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with Tag " + tag + " doesnt exist");
            return null;
        }

        GameObject objToSpawn = poolDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = pos;
        //objToSpawn.transform.SetParent(Grid.transform);
        objToSpawn.GetComponent<ParticleSystem>().Play();
        poolDictionary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }

    public void HideAllGridChild()
    {
        for(int i = 0; i< Grid.transform.childCount; i++)
        {
            Grid.transform.GetChild(i).gameObject.SetActive(false);
        }
    }


    public void SpawnBG()
    {
        SpawnFromPool(tags[Random.Range(0, tags.Length)], transform.position);
    }
    public void SpawnTileMap()
    {
        SpawnFromPool(tags[Random.Range(2, tags.Length)], transform.position + new Vector3(GameManager.manager.spawn_tilemap_x,0,0));
    }
    public void SpawnCheckpointTilemap()
    {
        SpawnFromPool(tags[Random.Range(0, 1)], transform.position + new Vector3(100, 0, 0));
    }
    public void SpawnBossTM()
    {
        SpawnFromPool(tags[Random.Range(1, 2)], transform.position + new Vector3(100, 0, 0));
    }

    public void SpawnProjectile(Transform pos, Vector3 dir)
    {
        SpawnProjectile(tags[Random.Range(0, tags.Length)], pos.position, dir);
        ///Debug.Log("projectile spawned");
    }

    public void SpawnProjectile2(Transform pos)
    {
        SpawnProjectile2(tags[Random.Range(0, tags.Length)], pos);
        ///Debug.Log("projectile spawned");
    }

    public void SpawnParticle(Transform pos)
    {
        SpawnParticle(tags[Random.Range(0, tags.Length)], pos.position);
    }

    
    IEnumerator setfalse(GameObject obj)
    {
        yield return new WaitForSeconds(.1f);
        obj.SetActive(false);
    }

}
