using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;

    public bool playerDead;

    public int currentHealth;

    public Material deadMateral;



    void Start()
    {
        currentHealth = startingHealth;
        playerDead = false;
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

        if (currentHealth <= 0 && !playerDead)
        {
            PlayerDies();
        }

        Debug.Log("Player's Current health: " + currentHealth);
    }

    void PlayerDies()
    {
        Debug.Log("Player is dead.");
        playerDead = true;
        gameObject.GetComponent<MeshRenderer>().material = deadMateral;
        transform.Rotate(0, 0, 90, Space.Self);
    }

}
