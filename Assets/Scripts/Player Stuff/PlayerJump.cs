using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Stuff")]
    public float jumpforce;
    //public float ground_time;
    //public float ground_counter;
    public bool Jump1;
    public bool Jump2;
    //public ParticleSystem jump_particle;
    public bool holding_jump;
    public float fjump_remember;
    private float fjump_time;
    public float floating_time;
    private float floating_countdown;

    private bool groundTouch;
    private float holdrb2d;
    private Rigidbody2D rb2d;
    private PlayerCollisions player_collisions;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player_collisions = GetComponent<PlayerCollisions>();
        holdrb2d = rb2d.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        //JumpCheck();
        KeyboardInputJump();
        if(fjump_time >= 0)
        {
            fjump_time -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        JumpCheck();
        Floating();

        if (Jump1 && fjump_time > 0)
        {
            Jump1 = false;
            Jump();
        }
    }

    public void Jump()
    {
        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(Vector2.up * GameManager.manager.jump_force, ForceMode2D.Impulse);
        //jump_particle.Play();
        //SoundManager.sound_manager.jump_sfx.Play();
    }

    public void JumpCheck()
    {
        if (player_collisions.isGrounded)
        {
            if (!groundTouch)
            {
                Play_Land_Particle();
                groundTouch = true;
            }
            //ground_counter = ground_time;
        }
        else
        {
            /*
            if(ground_counter <= 0)
            {
                Jump1 = false;
            }
            */
            //ground_counter -= Time.deltaTime;
            if (holding_jump)
            {
                floating_countdown -= Time.deltaTime;
            }
            groundTouch = false;
        }  
    }

    public void Play_Land_Particle()
    {
        Jump1 = true;
        Jump2 = true;
        //particle in jump and land func should be different for final
        //jump_particle.Play();
    }

    public void KeyboardInputJump()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            if (!GameManager.manager.movingReset && !GameManager.manager.dead)
            {
                holding_jump = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!GameManager.manager.movingReset && !GameManager.manager.dead)
            {
                floating_countdown = floating_time;
                fjump_time = fjump_remember;
                if (!player_collisions.isGrounded && !Jump1 && Jump2 && fjump_time > 0)
                {
                    Jump2 = false;
                    Jump();
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            holding_jump = false;
        }
    }

    public void Pressed_Jump_Button()
    {
        if (!GameManager.manager.movingReset && !GameManager.manager.dead)
        {
            floating_countdown = floating_time;
            fjump_time = fjump_remember;
            if (!player_collisions.isGrounded && !Jump1 && Jump2 && fjump_time > 0)
            {
                Jump2 = false;
                Jump();
            }
        }
    }

    public void Floating()
    {
        if (holding_jump && rb2d.velocity.y < 0 && floating_countdown > 0)
        {
            rb2d.gravityScale = holdrb2d / 6;
        }
        else if(rb2d.velocity.y < 0)
        {
            rb2d.gravityScale = holdrb2d * 1.5f;
        }
        else
        {
            rb2d.gravityScale = holdrb2d;
        }

    }

}
