using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEvents : MonoBehaviour
{
    public static ProjectileEvents projectile_event;

    private void Awake()
    {
        projectile_event = this;
    }

    public event Action On_Hit_Enemy;
    public void Hit_Enemy()
    {
        if(On_Hit_Enemy != null)
        {
            On_Hit_Enemy();
        }
    }
}
