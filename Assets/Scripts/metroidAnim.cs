using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class metroidAnim : MonoBehaviour
{

    const string SHOOT_LEFT = "left";
    const string SHOOT_LEFT_UP = "left up";
    const string SHOOT_LEFT_DOWN = "left down";
    const string SHOOT_RIGHT = "right";
    const string SHOOT_RIGHT_UP = "right up";
    const string SHOOT_RIGHT_DOWN = "right down";
    const string SHOOT_UP = "up";
    const string SHOOT_DOWN = "down";


    public Joystick joystick;
    public float jumpforce;
    public float attack_delay;
    public ObjectPoolNS bullet_pool;
    public Transform[] shoot_point;
    private string current_state;
    private Vector2 joy_dir;
    private bool attacking;
    private bool shooting;


    private Animator anim;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        joy_dir = joystick.Direction;
        Aim_State();
        Shoot_State();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

    }
    
    private void Change_Anim_State(string newState)
    {
        if (current_state == newState)
        {
            return;
        }

        anim.Play(newState);
        current_state = newState;
    }

    private void Jump()
    {
        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        //Change_Anim_State(PLAYER_JUMP);
    }

    private void Aim_State()
    {
        if(joy_dir.x == 0 && joy_dir.y == 1)
        {
            Change_Anim_State(SHOOT_UP);
        }
        else if (joy_dir.x == 0 && joy_dir.y == -1)
        {
            Change_Anim_State(SHOOT_DOWN);
        }
        else if (joy_dir.x == -1 && joy_dir.y == 1)
        {
            Change_Anim_State(SHOOT_LEFT_UP);
        }
        else if (joy_dir.x == -1 && joy_dir.y == -1)
        {
            Change_Anim_State(SHOOT_LEFT_DOWN);
        }
        else if (joy_dir.x == -1 && joy_dir.y == 0)
        {
            Change_Anim_State(SHOOT_LEFT);
        }
        else if (joy_dir.x == 1 && joy_dir.y == 1)
        {
            Change_Anim_State(SHOOT_RIGHT_UP);
        }
        else if (joy_dir.x == 1 && joy_dir.y == -1)
        {
            Change_Anim_State(SHOOT_RIGHT_DOWN);
        }
        else
        {
            Change_Anim_State(SHOOT_RIGHT);
        }
    }

    private void Shoot_State()
    {
        if (joy_dir.x != 0 || joy_dir.y != 0)
        {
            shooting = true;
            Debug.Log("dir not null");
        }
        else
        {
            shooting = false;
        }

        if (shooting)
        {
            if (joy_dir.x == 0 && joy_dir.y == 1)//up
            {
                Attack(shoot_point[2]);
            }
            else if (joy_dir.x == 0 && joy_dir.y == -1)//down
            {
                Attack(shoot_point[6]);
            }
            else if (joy_dir.x == -1 && joy_dir.y == 1)//left up
            {
                Attack(shoot_point[3]);
            }
            else if (joy_dir.x == -1 && joy_dir.y == -1)//left down
            {
                Attack(shoot_point[5]);
            }
            else if (joy_dir.x == -1 && joy_dir.y == 0)//left
            {
                Attack(shoot_point[4]);
            }
            else if (joy_dir.x == 1 && joy_dir.y == 1)//right up
            {
                Attack(shoot_point[1]);
            }
            else if (joy_dir.x == 1 && joy_dir.y == -1)//right down
            {
                Attack(shoot_point[7]);
            }
            else if(joy_dir.x == 1 && joy_dir.y == 0)//right
            {
                Attack(shoot_point[0]);
            }
        }
    }

    private void Attack(Transform atkpos)
    {
        if (!attacking)
        {
            attacking = true;
            Invoke("Complete_Attack", attack_delay);
            Shoot(atkpos);
        }
    }

    private void Complete_Attack()
    {
        attacking = false;
    }

    void Shoot(Transform atkpos)
    {

        bullet_pool.SpawnProjectile(atkpos, joy_dir);
        //bullet_pool.SpawnProjectile2(atkpos);
    }
}
