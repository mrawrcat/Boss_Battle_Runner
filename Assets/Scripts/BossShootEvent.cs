using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootEvent : MonoBehaviour
{
    public ObjectPoolNS bullet_pool;
    public Transform shootPos;

    public void Boss_Shoot()
    {
        bullet_pool.SpawnProjectile2(shootPos);
    }
}
