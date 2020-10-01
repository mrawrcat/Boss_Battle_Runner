using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grubz_Movement : MonoBehaviour
{
    public float speed;
    public Vector2 move_direction;
    [Header("Collision Detection")]
    public Vector2 bottomOffset, topOffset, leftOffset, rightOffset;
    public Vector2 boxSize;

    [Header("Status")]
    public bool touching_ground;
    public bool touching_ceiling;
    public bool touching_left_wall;
    public bool touching_right_wall;

    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    public bool dozigzag;

    private bool going_up;
    private bool going_left;
    private float x_dir, y_dir;
    private CamShake shake;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        //going_up = false;
        //going_left = true;
        x_dir = -1;
        y_dir = -1;
        rb2d = GetComponent<Rigidbody2D>();
        shake = FindObjectOfType<CamShake>();
        //rb2d.velocity = move_direction * speed;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (going_up)
        {
            y_dir = 1;
        }
        else if(!going_up)
        {
            y_dir = -1;
        }

        if (going_left)
        {
            x_dir = -1;
        }
        else if (!going_left)
        {
            x_dir = 1;
        }
        */
        ZigZag();
    }

    private void FixedUpdate()
    {
        touching_ground = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, boxSize, 0, whatIsGround);
        touching_ceiling = Physics2D.OverlapBox((Vector2)transform.position + topOffset, boxSize, 0, whatIsGround);
        touching_left_wall = Physics2D.OverlapBox((Vector2)transform.position + leftOffset, boxSize, 0, whatIsWall);
        touching_right_wall = Physics2D.OverlapBox((Vector2)transform.position + rightOffset, boxSize, 0, whatIsWall);
        move_direction = new Vector2(x_dir, y_dir);
        rb2d.velocity = move_direction * speed;
        
    }
    private void ZigZag()
    {

        if (touching_ground)
        {
            //change direction
            //going_up = !going_up;
            shake.Shake();
            y_dir = 1;
        }

        if (touching_ceiling)
        {
            shake.Shake();
            y_dir = -1;
        }

        if (touching_left_wall)
        {
            shake.Shake();
            // going_left = !going_left;
            x_dir = 1;
        }

        if (touching_right_wall)
        {
            shake.Shake();
            x_dir = -1;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + bottomOffset, boxSize);
        Gizmos.DrawWireCube((Vector2)transform.position + topOffset, boxSize);
        Gizmos.DrawWireCube((Vector2)transform.position + leftOffset, boxSize);
        Gizmos.DrawWireCube((Vector2)transform.position + rightOffset, boxSize);
    }

}
