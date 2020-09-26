using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_Move : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float move_speed = 10f;
    [SerializeField]
    private float x;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        move_speed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        if (transform.position.x <= -2 && transform.position.x >= -8)
        {
            rb2d.velocity = new Vector2(dir.x * move_speed, rb2d.velocity.y);
        }
        else if (transform.position.x < -8)
        {
            transform.position = new Vector2(-8, transform.position.y);
            //rb2d.velocity = new Vector2(5, rb2d.velocity.y);
        }
        else if (transform.position.x > -2)
        {
            transform.position = new Vector2(-2, transform.position.y);
            //rb2d.velocity = new Vector2(-5, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

    }
}
