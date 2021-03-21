using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;

    public bool playerDead;

    public int currentHealth;

    public Material deadMateral;

    public Slider healthSlider;

    public Text healthText;

    void Start()
    {
        currentHealth = startingHealth;
        playerDead = false;
        healthSlider.value = currentHealth;
        healthText.text = currentHealth.ToString(); 
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
            Mathf.Clamp(currentHealth, 0, startingHealth);
            healthSlider.value = currentHealth;
            healthText.text = currentHealth.ToString();
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
