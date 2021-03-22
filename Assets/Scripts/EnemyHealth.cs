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


    public float xpPoints = 20f;

    void Start()
    {
        currentHealth = startingHealth;
        enemyDead = false;
        anim = GetComponent<Animator>();
    }

    /*
    void Update()
    {

    } */

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

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevel>().GainExperience(xpPoints);
        //gameObject.GetComponent<MeshRenderer>().material = deadMateral;
        anim.SetInteger("animState", 3);
        //Destroy(gameObject, 3);
        
    }
   

    private void OnDestroy()
    {
        //instantiate particle system prefab.
    }

}
