using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;

    public bool enemyDead;

    public int currentHealth;
    public GameObject poof;

    Animator anim;
    
    //public Material deadMateral;
    private Quaternion preDeathRotation;

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
        
        preDeathRotation = Quaternion.Euler(90, 0.0f, 0.0f);
        
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevel>().GainExperience(xpPoints);
        //gameObject.GetComponent<MeshRenderer>().material = deadMateral;
        anim.SetInteger("animState", 3);
        var animDuration = anim.GetCurrentAnimatorStateInfo(0).length;
        Invoke("OnDestroy", animDuration + 1);
        
    }
   

    private void OnDestroy()
    {
        Instantiate(poof, transform.position + new Vector3(0, 0, 1.5f), preDeathRotation);
        Destroy(gameObject);
    }

}
