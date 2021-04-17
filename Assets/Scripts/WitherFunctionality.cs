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
            var enemy = collision.gameObject;
            Debug.Log("Enemy Withered! (Collision)");
            var enemyMove = enemy.GetComponent<EnemyMovement>();
            bool checkOnScreen = enemyMove.onScreen;
            if (checkOnScreen)
            {
                var enemyHealth = enemy.GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(damageAmount);

                var player = GameObject.FindGameObjectWithTag("Player");
                var playerHealth = player.GetComponent<PlayerHealth>();
                playerHealth.GainHealth(healAmount);
            }
        }
    }

}
