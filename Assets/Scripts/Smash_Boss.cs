using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash_Boss : MonoBehaviour
{

    const string BOSS_IDLE = "smash idle";
    const string BOSS_ANT = "smash anticipation";
    const string BOSS_ATTACK = "smash attack";

    private Animator anim;
    private bool attacking;
    private float attack_delay;
    private float do_attack;
    private string current_state;

    public float health;
    public float active_transform, idle_transform;
    public GameObject bossWall;
    public GameObject boss_cutscene;
    private bool boss_battle;
    private bool boss_active;
    // Start is called before the first frame update
    void Start()
    {
        ProjectileEvents.projectile_event.On_Hit_Enemy += Hit_Enemy;
        anim = GetComponentInChildren<Animator>();
        health = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            boss_battle = true;
        }

        if (boss_battle)
        {
            if (!boss_active)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(active_transform, transform.position.y), 10* Time.deltaTime);
                if(transform.position == new Vector3(active_transform, transform.position.y, transform.position.z) && !boss_active)
                {
                    bossWall.SetActive(true);
                    StartCoroutine("Play_Boss_Cutscene");
                    //boss_active = true;
                }
            }
            
        }

        if(health <= 0)
        {
            boss_battle = false;
            boss_active = false;
            bossWall.SetActive(false);
            transform.position = new Vector2(idle_transform, transform.position.y);
            if(transform.position == new Vector3(idle_transform, transform.position.y, transform.position.z))
            {
                health = 10;
            }
        }

        if (boss_active)
        {
            if (!attacking)
            {
                do_attack -= Time.deltaTime;
            }

            if (do_attack <= 0)
            {
                Anticipate();
                do_attack = 5f;
                //Attack();
                //attacking = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            boss_cutscene.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            boss_cutscene.SetActive(false);
        }
    }

    private void Anticipate()
    {
        if (!attacking)
        {
            attacking = true;
            Change_Anim_State(BOSS_ANT);
            attack_delay = anim.GetCurrentAnimatorStateInfo(0).length;
            Invoke("Attack", attack_delay);
        }

    }

    private void Attack()
    {
        Change_Anim_State(BOSS_ATTACK);
        attack_delay = anim.GetCurrentAnimatorStateInfo(0).length;
        Invoke("Back_To_Idle", attack_delay);
    }

    private void Back_To_Idle()
    {
        attacking = false;
        Change_Anim_State(BOSS_IDLE);
        
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
    IEnumerator Play_Boss_Cutscene()
    {
        boss_cutscene.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        boss_cutscene.SetActive(false);
        boss_active = true;
        //yield return new WaitForSecondsRealtime(.5f);
    }
    private void Hit_Enemy()
    {
        if (boss_active)
        {
            health--;
            Debug.Log("player projectile Hit enemy. enemy health: " + health);
        }
    }
}
