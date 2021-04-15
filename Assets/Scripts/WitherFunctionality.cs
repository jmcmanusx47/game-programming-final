using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitherFunctionality : MonoBehaviour
{
    public int damageAmount = 20;
    public int healAmount = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Withered! (Collision)");
            var enemyMove = collision.gameObject.
                    GetComponent<EnemyMovement>();
            bool checkOnScreen = enemyMove.onScreen;
            if (checkOnScreen)
            {
                var enemy = collision.gameObject;
                var enemyHealth = enemy.GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(damageAmount);

                var player = GameObject.FindGameObjectWithTag("Player");
                var playerHealth = player.GetComponent<PlayerHealth>();
                playerHealth.GainHealth(healAmount);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Withered! (Trigger)");
            var enemyMove = other.gameObject.
                    GetComponent<EnemyMovement>();
            bool checkOnScreen = enemyMove.onScreen;
            if (checkOnScreen)
            {
                var enemy = other.gameObject;
                var enemyHealth = enemy.GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(damageAmount);

                var player = GameObject.FindGameObjectWithTag("Player");
                var playerHealth = player.GetComponent<PlayerHealth>();
                playerHealth.GainHealth(healAmount);
            }
        }
    }
}
