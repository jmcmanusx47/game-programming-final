using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;

    public bool enemyDead;

    public int currentHealth;

    Animator anim;

    //public Material deadMateral;



    void Start()
    {
        currentHealth = startingHealth;
        enemyDead = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
        }

        if (currentHealth <= 0 && !enemyDead)
        {
            EnemyDies();
        }

        Debug.Log("Current health: " + currentHealth);
    }

    void EnemyDies()
    {
        Debug.Log("Enemy is dead.");
        enemyDead = true;
        //gameObject.GetComponent<MeshRenderer>().material = deadMateral;
        anim.SetInteger("animState", 3);
    }

}
