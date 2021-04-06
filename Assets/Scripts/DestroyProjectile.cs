using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{

    public float delay = 3;
    public int originalProjectileDamage = 20;
    public int projectileDamage = 20;
    public int manaRegen = 2;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        manaRegen = GlobalControl.Instance.currentManaRegen;
        player = GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, delay);
    }
    
    /*
    // Update is called once per frame
    void Update()
    {

    } */

    //There is probably a better place for this function, might be worthwhile to make it its own script for clarity's sake.
    private void OnTriggerEnter(Collider other)
    {
        //MAKE SURE THE ENEMIES ARE ACTUALLY TAGGED OR ELSE IT WONT DO DAMAGE!!!!!
        if (other.gameObject.CompareTag("Enemy"))
        {
            var enemyMove = other.gameObject.GetComponent<EnemyMovement>();
            var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            bool isDead = enemyHealth.enemyDead;
            bool checkOnScreen = enemyMove.onScreen;
            if (checkOnScreen && !isDead)
            {
                enemyHealth.TakeDamage(projectileDamage);
                var playerSpells = player.GetComponent<PlayerSpells>();
                playerSpells.GainMana(manaRegen);
            }
        }
        if (!other.gameObject.CompareTag("Projectile") &&
            !other.gameObject.CompareTag("MainCamera"))
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 0.5f);
        }
        if (other.gameObject.CompareTag("Boss"))
        {
            var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            var bossFSM = other.gameObject.GetComponent<BossFSM>();
            bool isDead = enemyHealth.enemyDead;
            bool checkOnScreen = bossFSM.onScreen;
            if (checkOnScreen && !isDead)
            {
                enemyHealth.TakeDamage(projectileDamage);
                var playerSpells = player.GetComponent<PlayerSpells>();
                playerSpells.GainMana(manaRegen);
            }
        }
    }
}
