using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Joystick joystick;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);


        if(transform.position.x <= 8 && transform.position.x >= -8)
        {
            if (joystick.Horizontal >= .2f)
            {
                rb2d.velocity = new Vector2(joystick.Horizontal * GameManager.manager.move_speed, rb2d.velocity.y);
            }
            else if (joystick.Horizontal <= -.2f)
            {
                rb2d.velocity = new Vector2(joystick.Horizontal * GameManager.manager.move_speed, rb2d.velocity.y);
                //rb2d.AddForce(new Vector2(joystick.Horizontal * 2, rb2d.velocity.y));
            }
            else
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }
        else if (transform.position.x < -8)
        {
            //transform.position = new Vector2(-8, transform.position.y);
            rb2d.velocity = new Vector2(5, rb2d.velocity.y);
        }
        else if (transform.position.x > 8)
        {
            //rb2d.velocity = new Vector2(-5, rb2d.velocity.y);
            transform.position = new Vector2(8, transform.position.y);
        }
    }
}
