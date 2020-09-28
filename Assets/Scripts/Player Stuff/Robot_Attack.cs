using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_Attack : MonoBehaviour
{
    const string PLAYER_IDLE = "robot move";
    const string PLAYER_AIR = "robot still";
    const string PLAYER_ATTACK = "robot shoot";
    [SerializeField]
    private Transform atk_pos;

    [SerializeField]
    private float attack_delay;

    [SerializeField]
    private ObjectPoolNS bullet_pool;

    private PlayerCollisions player_collisions;
    private Animator anim;
    private bool attacking;
    private string current_state;
    // Start is called before the first frame update
    void Start()
    {
        player_collisions = GetComponentInParent<PlayerCollisions>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }

        if (!attacking && player_collisions.isGrounded)
        {
            Change_Anim_State(PLAYER_IDLE);
        }
        else if(!attacking && !player_collisions.isGrounded)
        {
            Change_Anim_State(PLAYER_AIR);
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
    private void Attack()
    {
        if (!attacking)
        {
            attacking = true;
            Change_Anim_State(PLAYER_ATTACK);
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
        //bullet_pool.SpawnProjectile(atk_pos, atk_pos);
    }
}
