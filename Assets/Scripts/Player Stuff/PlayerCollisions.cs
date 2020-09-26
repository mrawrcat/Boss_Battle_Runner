using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [Header("Collision Detection")]
    public Vector2 bottomOffset;
    public Vector2 boxSize;
    public Vector2 inWallOffset;
    public Vector2 inWallBoxsize;

    [Header("Status")]
    public bool isGrounded;
    public bool insideWall;

    [Header("Layers")]
    public LayerMask whatIsGround;
    public LayerMask inWall;

    [Header("Colliders")]
    private BoxCollider2D normal_collider;
    //public GameObject revive_collider;
    //public GameObject revive_ground;

    private void Start()
    {
        normal_collider = GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CollisionDetection();
    }

    public void CollisionDetection()
    {
        insideWall = Physics2D.OverlapBox((Vector2)transform.position + inWallOffset, inWallBoxsize, 0, inWall);
        if (!GameManager.manager.dead && !GameManager.manager.movingReset)
        {
            isGrounded = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, boxSize, 0, whatIsGround);

            if (insideWall)
            {
                //normal_collider.isTrigger = true;
                //revive_collider.SetActive(true);
                //revive_ground.SetActive(true);
            }
            else
            {
                normal_collider.isTrigger = false;
                //revive_collider.SetActive(false);
                //revive_ground.SetActive(false);
            }

            //if invincible
            if (GameManager.manager.invincible > 0)
            {
                //normal_collider.isTrigger = true;
                //revive_collider.SetActive(true);
                //revive_ground.SetActive(true);
            }
            else
            {
                if (!insideWall)
                {
                    normal_collider.isTrigger = false;
                    //revive_collider.SetActive(false);
                    //revive_ground.SetActive(false);
                }
            }
        }
        else if(GameManager.manager.dead || GameManager.manager.movingReset)
        {
            normal_collider.isTrigger = true;
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + bottomOffset, boxSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube((Vector2)transform.position + inWallOffset, inWallBoxsize);
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere((Vector2)Atk_Pos.position, Atk_Radius);
    }
}
