﻿using System.Collections;
using System.Collections.Generic;
using DigitalRuby.LightningBolt;
using UnityEngine;

public class SpellEffects : MonoBehaviour
{
    public string spellName;
    public float delay;
    public int cost;

    GameObject player;
    bool playerStopped = false;
    float playerSpeed = 0;
    PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStopped)
        {
            controller.speed = 0;
        }
        else if (controller != null)
        {
            controller.speed = playerSpeed;
        }
    }

    public void InvokeSpell() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<PlayerController>();
        playerSpeed = controller.speed;
        Invoke(spellName, delay);
    }

    public void Fireball()
    {
        var pos = player.transform.position;
        pos.y += 1.5f;
        GameObject projectile = Instantiate(gameObject,
                pos + player.transform.forward,
                player.transform.rotation);

        projectile.transform.SetParent(
            GameObject.FindGameObjectWithTag("ProjectileParent").transform);
    }

    public void CloudKill()
    {
        var pos = player.transform.position;
        pos.z += 2.5f;
        GameObject cloud = Instantiate(gameObject, pos,
            player.transform.rotation);
        Destroy(cloud, 3f);
    }

    public void LightningBolt()
    {
        SetPlayerStop();
        var lightningScript = gameObject.GetComponent<LightningBoltScript>();
        var pos = player.transform.position;
        lightningScript.StartPosition.x = pos.x;
        lightningScript.EndPosition.x = pos.x;
        lightningScript.StartPosition.z = pos.z;
        lightningScript.EndPosition.z = pos.z + 40;
        var rotate = player.transform.rotation;
        GameObject projectile = Instantiate(gameObject, pos, rotate);

        Destroy(projectile, 0.2f);
        Invoke("SetPlayerStop", 0.75f);
    }

    public void Heal()
    {
        // Cool Spell Effect
        var pos = player.transform.position;
        GameObject heal = Instantiate(gameObject, pos,
            player.transform.rotation);
        var playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(-50);
        Destroy(heal, 1f);
    }

    public void SetPlayerStop()
    {
        playerStopped = !playerStopped;
    }

}
