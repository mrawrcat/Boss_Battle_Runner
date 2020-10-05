using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_Jump : MonoBehaviour
{
    const string PLAYER_RUN = "robot run";
    const string PLAYER_JUMP = "robot jump";


    [SerializeField]
    private float jumpforce = 10;

    private Animator anim;
    private Rigidbody2D rb2d;
    private PlayerCollisions player_collision;
    private string current_state;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponentInParent<Rigidbody2D>();
        player_collision = GetComponentInParent<PlayerCollisions>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
   
    private void FixedUpdate()
    {
        if (player_collision.isGrounded)
        {
            Change_Anim_State(PLAYER_RUN);
        }
        else if (!player_collision.isGrounded)
        {
            Change_Anim_State(PLAYER_JUMP);
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
}
