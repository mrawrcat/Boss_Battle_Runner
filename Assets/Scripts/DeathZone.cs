using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private CamShake shake;
    // Start is called before the first frame update
    void Start()
    {
        shake = FindObjectOfType<CamShake>();
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            shake.Shake();
            Debug.Log("player hit deathzone");
            GameManager.manager.finalSpeed = 0;
        }
    }
}
