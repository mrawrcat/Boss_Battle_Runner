using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAt : MonoBehaviour
{
    const string SHOOT_LEFT = "left";
    const string SHOOT_LEFT_UP = "left up";
    const string SHOOT_LEFT_DOWN = "left down";
    const string SHOOT_RIGHT = "right";
    const string SHOOT_RIGHT_UP = "right up";
    const string SHOOT_RIGHT_DOWN = "right down";
    const string SHOOT_UP = "up";
    const string SHOOT_DOWN = "down";

    [SerializeField]
    private Vector2 mouse_pos;
    [SerializeField]
    private Vector2 shoot_dir;

    public float attack_delay;
    public ObjectPoolNS bullet_pool;
    public Transform[] shoot_point;
    private string current_state;
    private bool attacking;
    private bool shooting;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Aim_State();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot_State();
        }

    }

    private void Aim_State()
    {
        if(mouse_pos.x >= 4 && mouse_pos.y >= 4)
        {
            Change_Anim_State(SHOOT_RIGHT_UP);
            shoot_dir = new Vector2(1, 1);
        }
        else if(mouse_pos.x < -4 && mouse_pos.y >= 4)
        {
            Change_Anim_State(SHOOT_LEFT_UP);
            shoot_dir = new Vector2(-1, 1);
        }
        else if(mouse_pos.x >= 4 && mouse_pos.y < -4)
        {
            Change_Anim_State(SHOOT_RIGHT_DOWN);
            shoot_dir = new Vector2(1, -1);
        }
        else if(mouse_pos.x < -4 && mouse_pos.y < -4)
        {
            Change_Anim_State(SHOOT_LEFT_DOWN);
            shoot_dir = new Vector2(-1, -1);
        }
        else if(mouse_pos.x >= -4 && mouse_pos.x < 4 && mouse_pos.y >= 0)
        {
            Change_Anim_State(SHOOT_UP);
            shoot_dir = new Vector2(0, 1);
        }
        else if(mouse_pos.x >= -4 && mouse_pos.x < 4 && mouse_pos.y < 0)
        {
            Change_Anim_State(SHOOT_DOWN);
            shoot_dir = new Vector2(0, -1);
        }
        else if(mouse_pos.x >= 4 && mouse_pos.y < 4 && mouse_pos.y >= -4)
        {
            Change_Anim_State(SHOOT_RIGHT);
            shoot_dir = new Vector2(1, 0);
        }
        else if (mouse_pos.x < -4 && mouse_pos.y < 4 && mouse_pos.y >= -4)
        {
            Change_Anim_State(SHOOT_LEFT);
            shoot_dir = new Vector2(-1, 0);
        }
        else
        {
            Change_Anim_State(SHOOT_RIGHT);
            shoot_dir = new Vector2(1, 0);
        }
    }

    private void Shoot_State()
    {
        if (mouse_pos.x >= 4 && mouse_pos.y >= 4)
        {
            Attack(shoot_point[1]);
        }
        else if (mouse_pos.x < -4 && mouse_pos.y >= 4)
        {
            Attack(shoot_point[3]);
        }
        else if (mouse_pos.x >= 4 && mouse_pos.y < -4)
        {
            Attack(shoot_point[7]);
        }
        else if (mouse_pos.x < -4 && mouse_pos.y < -4)
        {
            Attack(shoot_point[5]);
        }
        else if (mouse_pos.x >= -4 && mouse_pos.x < 4 && mouse_pos.y >= 0)
        {
            Attack(shoot_point[2]);
        }
        else if (mouse_pos.x >= -4 && mouse_pos.x < 4 && mouse_pos.y < 0)
        {
            Attack(shoot_point[6]);
        }
        else if (mouse_pos.x >= 4 && mouse_pos.y < 4 && mouse_pos.y >= -4)
        {
            Attack(shoot_point[0]);
        }
        else if (mouse_pos.x < -4 && mouse_pos.y < 4 && mouse_pos.y >= -4)
        {
            Attack(shoot_point[4]);
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
        bullet_pool.SpawnProjectile(atkpos, shoot_dir);
        //bullet_pool.SpawnProjectile2(atkpos);
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
}
