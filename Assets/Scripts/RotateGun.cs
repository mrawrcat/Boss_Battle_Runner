using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGun : MonoBehaviour
{
    [SerializeField]
    private ObjectPoolNS bullet_pool;
    public Transform stay_pos;
    public Transform atkpos;
    public Joystick joystick;
    private Rigidbody2D rb2d;
    private bool attacking;
    public bool shooting;
    public float attack_delay;
    public Vector2 joy_dir;
    public GameObject bullet_prefab;

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

        //joy_dir = new Vector2(joystick.Horizontal, joystick.Vertical);
        joy_dir = joystick.Direction;


        if(joy_dir.x != 0 || joy_dir.y != 0)
        {
            shooting = true;
            Debug.Log("dir not null");
        }
        else
        {
            shooting = false;
        }
       
       transform.position = stay_pos.position;
    }

    private void FixedUpdate()
    {
      
        if(joy_dir.x == 0 && joy_dir.y == 0)
        {
            Vector2 look_vec = new Vector2(joy_dir.x, joy_dir.y);
            float angle = Mathf.Atan2(look_vec.x, look_vec.y) * Mathf.Rad2Deg;
            rb2d.rotation = -angle;
        }
        else
        {

            Vector2 look_vec = new Vector2(joy_dir.x, joy_dir.y);
            float angle = Mathf.Atan2(look_vec.x, look_vec.y) * Mathf.Rad2Deg - 90;
            rb2d.rotation = -angle;
            if(shooting)
            {
                Attack();
            }
        }
        
        //Vector2 look_at_crosshair = new Vector2(crosshair.x, crosshair.y);
        //Vector2 look_vec = look_at_crosshair - rb2d.position;

        //Vector2 obj_pos = new Vector2(transform.position.x, transform.position.y);
        
        /*
        Vector3 cur_rot = Vector3.left * joy_dir.x + Vector3.down * joy_dir.y;
        Quaternion player_rot = Quaternion.LookRotation(cur_rot, Vector3.forward);
        rb2d.SetRotation(player_rot);
        */
    }

    private void Attack()
    {
        if (!attacking)
        {
            attacking = true;
            //Change_Anim_State(PLAYER_ATTACK);
            //atk_delay = anim.GetCurrentAnimatorStateInfo(0).length;
            Invoke("Complete_Attack", attack_delay);
            Shoot();
        }
    }

    private void Complete_Attack()
    {
        attacking = false;
    }

    void Shoot()
    {

        //bullet_pool.SpawnProjectile(atkpos, joy_dir);
        bullet_pool.SpawnProjectile2(atkpos);
    }


}
