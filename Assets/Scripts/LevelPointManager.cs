using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelPointManager : MonoBehaviour
{
    public int levelPoints = 0;
    public Button[] moveSpeed;
    public Button[] fireRate;
    public Button[] attackDamage;
    public Button[] damageReduction;
    public Button[] manaRegen;
    public Button[] spellSlots;
    public Button[] buySpellsValues;
    public Dictionary<string, Button> buySpells;
    public static HashSet<string> purchasedSpells;
    public int maxMoveSpeed = 0;
    public int maxFireRate = 0;
    public int maxAttackDamage = 0;
    public int maxDamageReduction = 0;
    public int maxManaRegen = 0;
    public int maxSpellSlots = 0;
    public Text levelPointText;
    // Start is called before the first frame update
    private void Awake()
    {
        buySpells = new Dictionary<string, Button>();
        if (purchasedSpells == null)
        {
            purchasedSpells = new HashSet<string>();
        }
        for(int i = 0; i < buySpellsValues.Length; i++)
        {
            var shop = gameObject.GetComponent<ShopFunctionality>();
            string key = shop.spellKeys[i];
            Button val = buySpellsValues[i];
            buySpells.Add(key, val);
        }
    }

    void Start()
    {
        levelPoints = GlobalControl.Instance.levelPoints;
        UpdateLevelPointText();
        UpdateAvailable();
    }

    public void UpdateAvailable()
    {
        SetSpells();
        SetAvailable(moveSpeed, maxMoveSpeed);
        SetAvailable(fireRate, maxFireRate);
        SetAvailable(attackDamage, maxAttackDamage);
        SetAvailable(damageReduction, maxDamageReduction);
        SetAvailable(manaRegen, maxManaRegen);
        SetAvailable(spellSlots, maxSpellSlots);
    }

    void SetAvailable(Button[] arr, int max)
    {
        for (int i = 0; i < 5; i++)
        {
            Button current = arr[i];
            if (i == max && max < levelPoints)
            {
                current.interactable = true;
            }
            else
            {
                current.interactable = false;
            }
        }
    }

    void SetSpells()
    {
        foreach(KeyValuePair<string, Button> pair in buySpells)
        {
            string key = pair.Key;
            Button val = pair.Value;
            var shop = gameObject.GetComponent<ShopFunctionality>();
            if (levelPoints < shop.spellCost ||
                purchasedSpells.Contains(key))
            {
                val.interactable = false;
            }
        }
    }

    public void UpdateLevelPointText()
    {
        levelPointText.text = "" + levelPoints;
    }
}
