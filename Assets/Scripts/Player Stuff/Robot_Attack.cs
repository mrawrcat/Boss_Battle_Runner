using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_Attack : MonoBehaviour
{
    const string PLAYER_IDLE = "robot still";
    const string PLAYER_ATTACK = "robot shoot";

    [SerializeField]
    private float attack_delay;

    private Animator anim;
    private bool attacking;
    private string current_state;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }

        if (!attacking)
        {
            Change_Anim_State(PLAYER_IDLE);
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
        }
    }

    private void Complete_Attack()
    {
        attacking = false;
    }
}
