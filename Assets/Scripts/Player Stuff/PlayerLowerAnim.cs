using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLowerAnim : MonoBehaviour
{

    private Animator anim;
    private PlayerCollisions player_collisions;
    private PlayerJump player_jump;
    private Rigidbody2D rb2d;
    private float velY;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player_collisions = GetComponentInParent<PlayerCollisions>();
        rb2d = GetComponentInParent<Rigidbody2D>();
        player_jump = GetComponentInParent<PlayerJump>();
    }

    // Update is called once per frame
    void Update()
    {
        velY = rb2d.velocity.y;
        SetAnims();
    }

    public void SetAnims()//sets animation variables for walk and jump; attack animation is set in PlayerAttack script
    {
        anim.SetFloat("Vertical Velocity", velY);
        anim.SetBool("isGrounded", player_collisions.isGrounded);
    }
}
