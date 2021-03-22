﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffects : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InvokeSpell(string spellName, float delay) 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Invoke(spellName, delay);
    }

    public void Fireball()
    {
        GameObject projectile = Instantiate(gameObject,
                player.transform.position + player.transform.forward,
                player.transform.rotation);

        projectile.transform.SetParent(
            GameObject.FindGameObjectWithTag("ProjectileParent").transform);
    }

}