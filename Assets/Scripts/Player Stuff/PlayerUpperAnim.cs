using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpperAnim : MonoBehaviour
{
    private PlayerAttack player_attack;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player_attack = GetComponentInParent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnims();
    }

    public void SetAnims()//sets animation variables for walk and jump; attack animation is set in PlayerAttack script
    {
        //anim.SetBool("holding_atk", player_attack.holding_atk);
        anim.SetFloat("holding_atk_timer", player_attack.holding_atk_timer);
    }
}
