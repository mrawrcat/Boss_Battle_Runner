using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squashstretch : MonoBehaviour
{
    const string PLAYER_RUN = "squash stretch run";
    const string PLAYER_JUMP = "squash stretch stretch";
    const string PLAYER_LAND = "squash stretch squash";

    public LayerMask whatIsGround;
    public Transform collisionOrigin;
    [SerializeField]
    private Vector2 boxSize;
    [SerializeField]
    private float jumpforce = 10;
    [SerializeField]
    private bool isGrounded;
    private bool groundTouch;
    private float change_state_delay;
    private string current_state;
    private Animator anim;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponentInParent<Rigidbody2D>();
        
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
        isGrounded = Physics2D.OverlapBox((Vector2)collisionOrigin.position, boxSize, 0, whatIsGround);
        if (!isGrounded)
        {
            groundTouch = false;
        }
        Squash();
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
    private void Squash()
    {
        if(isGrounded && !groundTouch)
        {
            groundTouch = true;
            Change_Anim_State(PLAYER_LAND);
            change_state_delay = anim.GetCurrentAnimatorStateInfo(0).length;
            Invoke("Run", .1f);

        }
    }

    private void Run()
    {
        Change_Anim_State(PLAYER_RUN);
    }

    public void Jump()
    {
        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        Change_Anim_State(PLAYER_JUMP);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)collisionOrigin.position, boxSize);
       
    }
}
