using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public float speed = 20f;
    //private Rigidbody2D rb2d;
    private void Start()
    {
        //rb2d = GetComponent<Rigidbody2D>();
        //rb2d.velocity = transform.right * speed;
    }
    private void FixedUpdate()
    {
        //rb2d.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("bullet hit enemy");
            ProjectileEvents.projectile_event.Hit_Enemy();
            gameObject.SetActive(false);
        }
    }
}
