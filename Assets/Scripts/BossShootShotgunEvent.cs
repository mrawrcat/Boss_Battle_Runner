using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootShotgunEvent : MonoBehaviour
{
    public ObjectPoolNS bullet_pool;
    public Transform[] shootPos;

    public void Boss_Shoot()
    {
        bullet_pool.SpawnProjectile2(shootPos[0]);
    }

    public void Boss_Shoot_Shotgun()
    {
        bullet_pool.SpawnProjectile(shootPos[0], new Vector3(1, 1, 0));
        bullet_pool.SpawnProjectile(shootPos[1], new Vector3(0, 1, 0));
        bullet_pool.SpawnProjectile(shootPos[2], new Vector3(-1, 1, 0));
    }
}
