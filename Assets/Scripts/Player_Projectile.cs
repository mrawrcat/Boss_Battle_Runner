using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Projectile : MonoBehaviour
{
    public Vector3 dir;
    public float speed;

    private void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        { 
            ProjectileEvents.projectile_event.Hit_Enemy();
            gameObject.SetActive(false);
        }
    }

    
}
