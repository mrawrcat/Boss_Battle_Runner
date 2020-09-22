using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IEnemy
{
    public int ID { get; set; }
    public Transform start_pos;
    //private ObjectPoolNS box_particles;
    // Start is called before the first frame update
    void Start()
    {
        ID = 1;
        //box_particles = GameObject.Find("Particles").GetComponent<ObjectPoolNS>();
        transform.position = start_pos.position;
    }
    public void Die()
    {
        //box_particles.SpawnParticle(transform);
        CombatEvents.EnemyDied(this);
        Debug.Log("killed box");
        gameObject.SetActive(false);

    }
}
