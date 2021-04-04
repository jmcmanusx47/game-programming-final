using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

    // Player Save
    public int currentHealth;
    public int currentMana;
    public float currentExperience;
    public int level;
    public int levelPoints;
    public float originalSpeed;
    public float currentSpeed;
    public float originalFireRate;
    public float currentFireRate;
    public int originalDamage;
    public int currentDamage;
    public int originalManaRegen;
    public int currentManaRegen;
    public float damageReduction;
    // Spells
    public GameObject QSpell;
    public GameObject ESpell;
    public GameObject RSpell;
    public GameObject ShiftSpell;
    public Image QSpellIcon;
    public Image ESpelIcon;
    public Image RSpellIcon;
    public Image ShiftSpellIcon;
    // Shop Save
    public Button[] pressedButtons;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
