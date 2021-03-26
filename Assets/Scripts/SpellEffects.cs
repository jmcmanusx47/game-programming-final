using System.Collections;
using System.Collections.Generic;
using DigitalRuby.LightningBolt;
using UnityEngine;

public class SpellEffects : MonoBehaviour
{

    public string spellName;
    public float delay;
    public int cost;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InvokeSpell(GameObject playerPrefab) 
    {
        if (player == null)
        {
            player = playerPrefab;
        }
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
        var lightningScript = gameObject.GetComponent<LightningBoltScript>();
        var pos = player.transform.position;
        lightningScript.StartPosition.x = pos.x;
        lightningScript.EndPosition.x = pos.x;
        lightningScript.StartPosition.z = pos.z;
        lightningScript.EndPosition.z = pos.z + 40;
        GameObject projectile = Instantiate(gameObject, pos,
            player.transform.rotation);

        var freeze = gameObject.GetComponent<PlayerFreeze>();
        freeze.controller = player.GetComponent<PlayerController>();
        freeze.Freeze();
        Destroy(projectile, 0.2f);
    }

    public void Heal()
    {
        var pos = player.transform.position;
        GameObject heal = Instantiate(gameObject, pos,
            player.transform.rotation);
        var playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(-50);
        Destroy(heal, 1.0f);
    }

}
