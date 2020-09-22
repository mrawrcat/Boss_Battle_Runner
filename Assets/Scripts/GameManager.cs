using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public float finalSpeed;
    private float savefinalSpeed;
    public float coins;
    public float total_coins;
    public float distanceMoved;
    public float invincible;

    [Header("Player Stats")]
    public float move_speed;
    public float jump_force;

    [Header("Tilemap Stuff")]
    public float turn_tilemap_off_x;
    public float spawn_tilemap_x;
    public float deadheight;
    [Header("Game/Player Status")]
    public bool dead;
    public bool movingReset;
    public bool isUnder;

    [Header("Boss Stuff")]
    public bool bossBattle;
    public float bossAppearDist;
    public int whichBoss;
    public bool spawned_checkpoint;
    public bool passed_checkpoint;

    [Header("Game Mode Stuff")]
    public bool enable_Boss;
    private float distMultiply;
   
    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        bossAppearDist = 50;
        savefinalSpeed = finalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        savefinalSpeed = finalSpeed;
        if (dead || movingReset)
        {
            finalSpeed = 0;
            
        }
        else
        {
            //finalSpeed = savefinalSpeed;
        }
        
        if(invincible > 0)
        {
            invincible -= Time.deltaTime;
        }

        if(distanceMoved > bossAppearDist && distanceMoved < bossAppearDist + 1)
        {
            if (enable_Boss)
            {
                bossBattle = true;
                //randomBoss();
            }
        }

        Calculate_Distance();
    }

    private void Calculate_Distance()
    {
        if (!dead && !movingReset)
        {
            GameManager.manager.distanceMoved += GameManager.manager.finalSpeed * Time.deltaTime;
        }
       
    }

    public void randomBoss()
    {
        whichBoss = Random.Range(0, 4);
    }
    public void SaveData()
    {
        SaveDataSystem.SaveGameData(this);
    }
    public void LoadData()
    {
        StoreData data = SaveDataSystem.loadData();

        total_coins = data.total_coins;
    }
}
