using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderBoss : MonoBehaviour, IEnemy
{
    public int ID { get; set; }
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        ID = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        CombatEvents.EnemyDied(this);
        Debug.Log("killed Boss");
        //gameObject.SetActive(false);
        
    }

    public void Take_Dmg()
    {
        health--;
        if (health <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "PlayerProjectile")
        {
            Take_Dmg();
        }
    }


}
