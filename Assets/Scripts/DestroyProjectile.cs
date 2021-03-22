﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{

    public float delay = 3;
    public int projectileDamage = 20;

    // Start is called before the first frame update
    void Start()
    {
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
            var enemyMove = other.GetComponent<EnemyMovement>();
            bool checkOnScreen = enemyMove.onScreen;
            if (checkOnScreen)
            {
                other.gameObject.GetComponent<EnemyHealth>().TakeDamage(projectileDamage);
            }
        }
        if (!other.gameObject.CompareTag("Projectile") &&
            !other.gameObject.CompareTag("MainCamera"))
        {
            Destroy(gameObject);
        }
    }
}
