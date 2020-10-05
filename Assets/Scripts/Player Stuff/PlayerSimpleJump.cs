using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSimpleJump : MonoBehaviour
{
    [SerializeField]
    private float jumpforce = 10;

    private Animator anim;
    private Rigidbody2D rb2d;
    private PlayerCollisions player_collision;
    private string current_state;
    private bool ground_touch;
    private bool attacking;
    private float atk_delay;

    const string PLAYER_RUN = "robot still run";
    const string PLAYER_JUMP = "robot still jump";
    const string PLAYER_ATTACK = "robot still jump shoot";

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        player_collision = GetComponent<PlayerCollisions>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        if (player_collision.isGrounded && !attacking)
        {
            Change_Anim_State(PLAYER_RUN);
        }
        else if(!player_collision.isGrounded && !attacking)
        {
            Change_Anim_State(PLAYER_JUMP);
        }
        
    }

    void Change_Anim_State(string newState)
    {
        if(current_state == newState)
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
            Invoke("Complete_Attack", .1f);
        }
    }

    private void Complete_Attack()
    {
        attacking = false;
    }

    private void Jump()
    {
        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        //Change_Anim_State(PLAYER_JUMP);
    }
}
