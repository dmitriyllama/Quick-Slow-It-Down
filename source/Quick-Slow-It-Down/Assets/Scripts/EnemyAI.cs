using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    private bool alive;
    void Start()
    {
        alive = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!alive) return;
        transform.LookAt(player, transform.up);
    }

    public void Die()
    {
        alive = false;
    }
}
