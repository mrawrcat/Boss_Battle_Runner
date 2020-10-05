using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grubz_Movement : MonoBehaviour
{
    public Transform player;
    public float tantrum_speed, idle_speed;
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

    private Vector2 player_pos;
    [SerializeField]
    private bool has_player_pos;
    public float see_random;
    [SerializeField]
    private float speed;
    private bool attacking;
    private bool can_attack;
    private float countdown;
    private bool going_left;
    private float x_dir, y_dir;
    private CamShake shake;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        //going_up = false;
        //going_left = true;
        //x_dir = -1;
        //y_dir = -1;
        can_attack = true;
        rb2d = GetComponent<Rigidbody2D>();
        shake = FindObjectOfType<CamShake>();
        move_direction.Normalize();
        //Tantrum();
        //rb2d.velocity = move_direction * speed;
    }

    // Update is called once per frame
    void Update()
    {
        //ZigZag();
        //rb2d.velocity = move_direction * speed;
        //Idle_Movement();
        //Random_State();


        countdown -= Time.deltaTime;

        if(countdown <= 0)
        {
            //Homing_Atk();
            countdown = 5f;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            //rb2d.velocity = Vector2.zero;
            rb2d.velocity *= 999f / 1000f;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Homing_Atk();
            //Attack_Player();
        }
        
    }

    private void FixedUpdate()
    {
        touching_ground = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, boxSize, 0, whatIsGround);
        touching_ceiling = Physics2D.OverlapBox((Vector2)transform.position + topOffset, boxSize, 0, whatIsGround);
        touching_left_wall = Physics2D.OverlapBox((Vector2)transform.position + leftOffset, boxSize, 0, whatIsWall);
        touching_right_wall = Physics2D.OverlapBox((Vector2)transform.position + rightOffset, boxSize, 0, whatIsWall);

    }



    private void ZigZag()
    {

        move_direction = new Vector2(x_dir, y_dir);
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


    private void Attack_Player()
    {
        //attacking = true;
        //can_attack = false; 
        speed = tantrum_speed;
        if (!has_player_pos)
        {
            player_pos = player.position - transform.position;
            player_pos.Normalize();
            has_player_pos = true;
        }
        if (has_player_pos)
        {
            //rb2d.velocity = player_pos * speed;
            rb2d.AddForce(player_pos * speed, ForceMode2D.Impulse);
        }
        if (touching_ground || touching_ceiling || touching_left_wall || touching_right_wall)
        {
            //rb2d.velocity = Vector2.zero;
            has_player_pos = false;
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
            //Idle_Movement();
            //StartCoroutine("transition_to_idle");
        }
    }

    private void Homing_Atk()
    {
        speed = tantrum_speed;
        rb2d.velocity = Vector2.zero;
        Debug.Log("did homing atk");
        player_pos = player.position - transform.position;
        player_pos.Normalize();
        //rb2d.velocity = player_pos * speed;
        rb2d.AddForce(player_pos * speed, ForceMode2D.Impulse);
        if (touching_ground || touching_ceiling || touching_left_wall || touching_right_wall)
        {
            StartCoroutine("transition_to_idle");
        }
    }

    private void Idle_Movement()
    {
        attacking = false;
        speed = idle_speed;
        rb2d.AddForce(move_direction * idle_speed, ForceMode2D.Impulse);
        //rb2d.velocity =  move_direction * idle_speed;
    }

    private void Tantrum()
    {
        speed = tantrum_speed;
        can_attack = false;
        attacking = true;
        if (touching_ground || touching_ceiling || touching_left_wall || touching_right_wall && attacking)
        {
            //rb2d.velocity = Vector2.zero;
            StartCoroutine("transition_to_idle");
        }
    }
    private void Random_State()
    {
        int random_state = Random.Range(0, 2);
        if (random_state == 0 && !attacking && can_attack)
        {
            see_random = random_state;
            //Attack_Player();
        }
        else if(random_state == 1 && !attacking && can_attack)
        {
            see_random = random_state;
            //Tantrum();
        }
    }

    IEnumerator transition_to_idle()
    {
        //Idle_Movement();
        yield return new WaitForSeconds(1f);
        rb2d.velocity = Vector2.zero;
        //can_attack = true;
        //attacking = false; 
        //Random_State();
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
