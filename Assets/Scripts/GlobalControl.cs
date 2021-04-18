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
    public float originalSpeed = 1;
    public float currentSpeed = 1;
    public float originalFireRate = .25f;
    public float currentFireRate = .25f;
    public int originalDamage = 20;
    public int currentDamage = 20;
    public int originalManaRegen = 2;
    public int currentManaRegen = 2;
    public float damageReduction = 1;
    // Spells
    public GameObject QSpell;
    public GameObject ESpell;
    public GameObject RSpell;
    public GameObject LSSpell;
    public Sprite QSpellIcon;
    public Sprite ESpellIcon;
    public Sprite RSpellIcon;
    public Sprite LSSpellIcon;
    public bool questionUpgrade = false;
    // Shop Save
    public Dictionary<string, GameObject> purchasedSpells;
    public int maxMoveSpeed = 0;
    public int maxFireRate = 0;
    public int maxAttackDamage = 0;
    public int maxDamageReduction = 0;
    public int maxManaRegen = 0;
    public int maxSpellSlots = 0;
    public bool QSpellUnlock = false;
    public bool ESpellUnlock = false;
    public bool RSpellUnlock = false;
    public bool LSSpellUnlock = false;

    // Scene Management
    public int currentSceneIndex = 2;

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
