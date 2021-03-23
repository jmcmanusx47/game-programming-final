using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudKillEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Colliding!");
        if(other.gameObject.CompareTag("Enemy"))
        {
            var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(1);
            Debug.Log(enemyHealth.currentHealth);
        }
    }
}
