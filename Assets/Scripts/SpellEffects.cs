using System.Collections;
using System.Collections.Generic;
using DigitalRuby.LightningBolt;
using UnityEngine;

public class SpellEffects : MonoBehaviour
{
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

    public void InvokeSpell(string spellName, float delay) 
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

    public void LightningBolt()
    {
        SetPlayerStop();
        var lightningScript = gameObject.GetComponent<LightningBoltScript>();
        var pos = player.transform.position;
        lightningScript.StartPosition.x = pos.x;
        lightningScript.EndPosition.x = pos.x;
        lightningScript.StartPosition.z = pos.z;
        lightningScript.EndPosition.z = pos.z + 40;
        GameObject projectile = Instantiate(gameObject, pos,
            player.transform.rotation);

        Destroy(projectile, 0.2f);
        Invoke("SetPlayerStop", 0.75f);
    }

    public void SetPlayerStop()
    {
        playerStopped = !playerStopped;
    }

}
