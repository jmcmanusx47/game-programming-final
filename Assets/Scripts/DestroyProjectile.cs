using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{

    public float delay = 3;
    public int projectileDamage = 20;
    public int manaRegen = 2;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
=======
        manaRegen = GlobalControl.Instance.currentManaRegen;
        player = GameObject.FindGameObjectWithTag("Player");
>>>>>>> Stashed changes
        Destroy(gameObject, delay);
    }

    // Update is called once per frame
    void Update()
    {

    }


    //There is probably a better place for this function, might be worthwhile to make it its own script for clarity's sake.
    private void OnTriggerEnter(Collider other)
    {
        //MAKE SURE THE ENEMIES ARE ACTUALLY TAGGED OR ELSE IT WONT DO DAMAGE!!!!!
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameObject enemy = other.gameObject;
            var enemyMove = enemy.gameObject.GetComponent<EnemyMovement>();
            var enemyHealth = enemy.gameObject.GetComponent<EnemyHealth>();
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
            Destroy(gameObject);
        }
    }

}
