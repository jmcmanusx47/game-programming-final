using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeOfColdFunctionality : MonoBehaviour
{
    public int damageAmount = 30;
    public float slowAmount = 0.5f;
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
            Debug.Log("Enemy Slowed! (Collision)");
            var enemyMove = enemy.GetComponent<EnemyMovement>();
            bool checkOnScreen = enemyMove.onScreen;
            if (checkOnScreen)
            {
                var enemyHealth = enemy.GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(damageAmount);

                enemyMove.Slow(slowAmount);
            }
        }
    }
}
