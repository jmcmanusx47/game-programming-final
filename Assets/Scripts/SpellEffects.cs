using System.Collections;
using System.Collections.Generic;
using DigitalRuby.LightningBolt;
using UnityEngine;

public class SpellEffects : MonoBehaviour
{
    public string spellName;
    public float delay;
    public int cost;
    public float duration;
    public AudioClip spellSFX;

    GameObject player;
    bool playerStopped = false;
    float playerSpeed = 0;
    PlayerController controller;
    PlayerSpells spells; 
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
        spells = player.GetComponent<PlayerSpells>();
        playerSpeed = controller.speed;
        AudioSource.PlayClipAtPoint(spellSFX, Camera.main.transform.position);
        Invoke(spellName, delay);
    }

    public void FireBall()
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
        GameObject projectile = Instantiate(gameObject, pos,
            player.transform.rotation);
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
        playerHealth.GainHealth(50);
        Destroy(heal, 1f);
    }

    public void ScatterShot()
    {
        var pos = player.transform.position;
        var scatterShot = Instantiate(gameObject, pos, player.transform.rotation);
        var shootProjectile = player.GetComponent<ShootProjectile>();
        shootProjectile.ScatterShot(duration);
        spells.buff = true;
        Invoke("CancelBuff", duration);
        Destroy(scatterShot, .75f);
    }

    public void LockOn()
    {
        var pos = player.transform.position;
        var lockOn = Instantiate(gameObject, pos, player.transform.rotation);
        var shootProjectile = player.GetComponent<ShootProjectile>();
        shootProjectile.LockOn(duration);
        spells.buff = true;
        Invoke("CancelBuff", duration);
        Destroy(lockOn, .75f);
    }

    public void Frenzy()
    {
        var pos = player.transform.position;
        var frenzy= Instantiate(gameObject, pos, player.transform.rotation);
        var shootProjectile = player.GetComponent<ShootProjectile>();
        var playerController = player.GetComponent<PlayerController>();
        shootProjectile.Frenzy(.5f, duration);
        playerController.Frenzy(2f, duration);
        spells.buff = true;
        Invoke("CancelBuff", duration);
        Destroy(frenzy, .75f);
    }

    public void ConeOfCold()
    {
        SetPlayerStop();
        var pos = player.transform.position;
        pos.z += 2;
        var coneOfCold = Instantiate(gameObject, pos,
            player.transform.rotation);
        Destroy(coneOfCold, 0.5f);
        Invoke("SetPlayerStop", 0.5f);
    }

    public void Wither()
    {
        var pos = player.transform.position;
        GameObject wither = Instantiate(gameObject, pos,
            player.transform.rotation);
        Destroy(wither, 0.75f);        
    }

    public void Fortify()
    {
        var pos = player.transform.position;
        var fortify = Instantiate(gameObject, pos, player.transform.rotation);
        var playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.Fortify(.5f, duration);
        spells.buff = true;
        Invoke("CancelBuff", duration);
        Destroy(fortify, .75f);
    }

    public void SetPlayerStop()
    {
        playerStopped = !playerStopped;
    }

    public void CancelBuff()
    {
        spells.buff = false;
    }

}
