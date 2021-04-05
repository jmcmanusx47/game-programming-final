using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;

    public bool playerDead;

    public int currentHealth = 100;

    public float damageReduction = 1;

    //public Material deadMateral;

    public Slider healthSlider;

    public Text healthText;
    public AudioClip deadSFX;

    Animator anim;

    void Start()
    {

        currentHealth = GlobalControl.Instance.currentHealth;

        damageReduction = GlobalControl.Instance.damageReduction;
        //currentHealth = startingHealth;
        playerDead = false;
        healthSlider.value = currentHealth;
        healthText.text = currentHealth.ToString();
        anim = GetComponent<Animator>();
    }

    /*
    void Update()
    {

    } */

    public void GainHealth(int healthAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth += healthAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth);
            healthSlider.value = currentHealth;
            healthText.text = currentHealth.ToString();
        }

        Debug.Log("Player's Current health: " + currentHealth);
    }

    public void TakeDamage(int damageAmount)
    {

        if (currentHealth > 0)
        {
            damageAmount = Mathf.FloorToInt(damageAmount * damageReduction);
            currentHealth -= damageAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth);
            healthSlider.value = currentHealth;
            healthText.text = currentHealth.ToString();
        }

        if (currentHealth <= 0 && !playerDead)
        {
            AudioSource.PlayClipAtPoint(deadSFX, transform.position);
            PlayerDies();
        }

        Debug.Log("Player's Current health: " + currentHealth);
    }

    void PlayerDies()
    {
        Debug.Log("Player is dead.");
        playerDead = true;
        //gameObject.GetComponent<MeshRenderer>().material = deadMateral;
        //transform.Rotate(0, 0, 90, Space.Self);
        anim.SetInteger("animState", 3);
        FindObjectOfType<LevelManager>().LevelLost();
    }

    public void SavePlayerHealth()
    {
        GlobalControl.Instance.currentHealth = currentHealth;
    }

}
