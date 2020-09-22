using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTilemap : MonoBehaviour
{
    
    
    private Rigidbody2D rb2d;
    
    // Start is called before the first frame update
    void Start()
    {
        //spawner = FindObjectOfType<Spawner>();
        //transform.Translate(Vector2.left * Time.deltaTime * GameManager.manager.speed);
        rb2d = GetComponent<Rigidbody2D>();
        //rb2d.velocity = Vector2.left * GameManager.manager.finalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector2.left * Time.deltaTime * GameManager.manager.speed);
        rb2d.velocity = Vector2.left * GameManager.manager.finalSpeed;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.tag == "SetTileFalse")
    //    {
    //        gameObject.SetActive(false);
    //    }
    //}




}
