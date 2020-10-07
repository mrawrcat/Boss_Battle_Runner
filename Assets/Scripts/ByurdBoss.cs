using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByurdBoss : MonoBehaviour
{

    const string BOSS_IDLE = "byurd idle";
    const string BOSS_UPSIDE_DOWN = "byurd upside down idle";
    const string BOSS_FLIP = "byurd flip";
    const string BOSS_ATTACK = "byurd upside down atk";
    const string BOSS_ANT = "byurd anticipation";

    public Transform[] idlepoints;
    [SerializeField]
    private int pointselection;
    [SerializeField]
    private Transform currentpoint;

    public GameObject boss_cutscene;

    [SerializeField]
    private float speed;

    private bool attacking;
    private float attack_delay;
    private string current_state;
    [SerializeField]
    private float state;


    private float health;
    private bool boss_active;
    private bool boss_battle;
    private bool just_attacked;
    private bool at_upside_down;
    private bool finished_cutscene;
    private Animator anim;
    private SpriteRenderer visual;
    // Start is called before the first frame update
    void Start()
    {
        //maybe i should subscribe when boss battle/active then unsubscribe when boss dead?
        ProjectileEvents.projectile_event.On_Hit_Enemy += Hit_Enemy;
        anim = GetComponentInChildren<Animator>();
        visual = GetComponentInChildren<SpriteRenderer>();
        health = 10;
        pointselection = 1;
        currentpoint = idlepoints[pointselection];
    }

    // Update is called once per frame
    void Update()
    {
        
        Flip_Visual();
        if (Input.GetKeyDown(KeyCode.P))
        {
            boss_battle = true;
            health = 10;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            health -= 10;
        }

        if (boss_battle)
        {
            if (!boss_active)
            {

                transform.position = Vector2.MoveTowards(transform.position, idlepoints[0].position, speed * Time.deltaTime);
                if (transform.position == idlepoints[0].position && !boss_active)
                {
                    StartCoroutine("Play_Boss_Cutscene");
                    Invoke("Finished_Cutscene", 1.5f);
                    boss_active = true;
                }
            }
        }

        if (health <= 0)
        {
            boss_battle = false;
            boss_active = false;
            finished_cutscene = false;
            at_upside_down = false;
            state = 0;
            pointselection = 1;
            currentpoint = idlepoints[pointselection];
            just_attacked = false;
            attacking = false;
            transform.position = new Vector2(20f, 1.5f);
            Change_Anim_State(BOSS_IDLE);

            if (transform.position == new Vector3(20f, 1.5f, transform.position.z))
            {
                //health = 10;
            }
        }

        if (!at_upside_down)
        {
            Change_Anim_State(BOSS_IDLE);
        }

        if (state == 1)
        {
            Move_To_Point();
        }

        if (boss_active && finished_cutscene)
        {
            At_Upper_Left_Point();
            At_Left_Point();
            At_Upper_Right_Point();
            At_Right_Point();
        }

        
        
    }

    private void Finished_Cutscene()
    {
        finished_cutscene = true;
    }

    private void Flip_Visual()
    {
        if(transform.position.x >= 0)
        {
            visual.flipX = false;
        }
        else
        {
            visual.flipX = true;
        }
    }

    private void Move_To_Point()
    {
        at_upside_down = false;
        transform.position = Vector2.MoveTowards(transform.position, currentpoint.position, speed * Time.deltaTime);
        if (transform.position == currentpoint.position)
        {
            pointselection++;
            state = 0;
            if (pointselection == idlepoints.Length)
            {
                pointselection = 0;
            }

            currentpoint = idlepoints[pointselection];
        }
    }

    private void At_Upper_Left_Point()
    {
        if (transform.position == idlepoints[1].position)
        {
            if (!at_upside_down)
            {
                Flip_And_Idle();
            }
        }
    }
   
    private void At_Left_Point()
    {
        if (transform.position == idlepoints[2].position)
        {
            Invoke("Go_Next_Point", 1f);
        }
    }

    private void At_Upper_Right_Point()
    {
        if (transform.position == idlepoints[3].position)
        {
            if (!at_upside_down)
            {
                Flip_And_Idle();
            }
        }
    }

    private void At_Right_Point()
    {
        if (transform.position == idlepoints[0].position)
        {
            Invoke("Go_Next_Point", 1f);
        }
    }

    IEnumerator Play_Boss_Cutscene()
    {
        boss_cutscene.SetActive(true);
        GameManager.manager.finalSpeed = 0;
        yield return new WaitForSeconds(1.5f);
        boss_cutscene.SetActive(false);
        GameManager.manager.finalSpeed = 10;
    }

    
    private void Anticipate()
    {
        if (!attacking)
        {
            attacking = true;
            Change_Anim_State(BOSS_ANT);
            attack_delay = anim.GetCurrentAnimatorStateInfo(0).length;
            Invoke("Shoot_Shotgun", attack_delay);

        }
    }

    private void Shoot_Shotgun()//doesnt actually shoot the shotgun just plays the animation that shoots the shotgun
    {
        Change_Anim_State(BOSS_ATTACK);
        attack_delay = anim.GetCurrentAnimatorStateInfo(0).length;
        Invoke("Upside_Down_Idle", attack_delay);
        just_attacked = true;
        if (just_attacked)
        {
            Invoke("Go_To_Side", .5f);
        }
        //attacking = false;
    }

    private void Flip_And_Idle()
    {
        Change_Anim_State(BOSS_FLIP);
        attack_delay = anim.GetCurrentAnimatorStateInfo(0).length;
        Change_Anim_State(BOSS_UPSIDE_DOWN);
        at_upside_down = true;
        StartCoroutine("Anticipate_And_Attack");
    }
    
    private IEnumerator Anticipate_And_Attack()
    {
        yield return new WaitForSeconds(1f);
        Anticipate();
    }

    private void Upside_Down_Idle()
    {
        Change_Anim_State(BOSS_UPSIDE_DOWN);
    }

    private void Go_To_Side()
    {
        //Change_Anim_State(BOSS_IDLE);
        at_upside_down = false;
        attacking = false;
        Go_Next_Point();
    }

    private void Go_Next_Point()
    {
        state = 1;
    }

    private void Hit_Enemy()
    {
        if (boss_active)
        {
            health--;
            Debug.Log("player projectile Hit enemy. enemy health: " + health);
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
}
