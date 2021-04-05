using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class ShopFunctionality : MonoBehaviour
{
    public float[] moveSpeedIncrements;
    public float[] fireRateIncrements;
    public float[] attackDamageIncrements;
    public float[] damageReductionIncrements;
    public int[] manaRegenIncrements;
    public int spellCost = 3;

    public Dropdown QSpellEquip;
    public Dropdown ESpellEquip;
    public Dropdown RSpellEquip;
    public Dropdown LSSpellEquip;
    public string[] spellKeys;
    public Sprite[] spellImages;
    public GameObject[] spellValues;
    public Dictionary<string, GameObject> allSpells;
    public Dictionary<string, Sprite> spellIcons;
    public Dictionary<string, GameObject> purchasedSpells;

    public Image QSpellIcon;
    public Image ESpellIcon;
    public Image RSpellIcon;
    public Image LSSpellIcon;

    public bool QSpellUnlock = false;
    public bool ESpellUnlock = false;
    public bool RSpellUnlock = false;
    public bool LSSpellUnlock = false;

    Color unequip;

    private void Awake()
    {
        allSpells = new Dictionary<string, GameObject>();
        spellIcons = new Dictionary<string, Sprite>();
        
        for (int i = 0; i < spellKeys.Length; i++)
        {
            string key = spellKeys[i];
            GameObject val = spellValues[i];
            Sprite image = spellImages[i];
            allSpells.Add(key, val);
            spellIcons.Add(key, image);
        }

        purchasedSpells = GlobalControl.Instance.purchasedSpells;
        if (purchasedSpells == null)
        {
            purchasedSpells = new Dictionary<string, GameObject>
            {
                { "None", null }
            };
        }
        var QIcon = GlobalControl.Instance.QSpellIcon;
        var EIcon = GlobalControl.Instance.ESpellIcon;
        var RIcon = GlobalControl.Instance.RSpellIcon;
        var LSIcon = GlobalControl.Instance.LSSpellIcon;

        unequip = QSpellIcon.color;

        if (QIcon != null)
        {
            QSpellIcon.sprite = QIcon;
            QSpellIcon.color = Color.white;
        }
        if (EIcon != null)
        {
            ESpellIcon.sprite = EIcon;
            ESpellIcon.color = Color.white;
        }
        if (RIcon != null)
        {
            RSpellIcon.sprite = RIcon;
            RSpellIcon.color = Color.white;
        }
        if (LSIcon != null)
        {
            LSSpellIcon.sprite = LSIcon;
            LSSpellIcon.color = Color.white;
        }
        UpdateAllDropdowns();
    }

    private void UpdateAllDropdowns()
    {
        if (QSpellUnlock)
        {
            QSpellEquip.interactable = true;
            UpdateDropdown(QSpellEquip, 0);
        }
        else
        {
            QSpellEquip.interactable = false;
        }
        if (ESpellUnlock)
        {
            ESpellEquip.interactable = true;
            UpdateDropdown(ESpellEquip, 1);
        }
        else
        {
            ESpellEquip.interactable = false;
        }
        if (RSpellUnlock)
        {
            RSpellEquip.interactable = true;
            UpdateDropdown(RSpellEquip, 2);
        }
        else
        {
            RSpellEquip.interactable = false;
        }
        if (LSSpellUnlock)
        {
            LSSpellEquip.interactable = true;
            UpdateDropdown(LSSpellEquip, 3);
        }
        else
        {
            LSSpellEquip.interactable = false;
        }
    }

    private void UpdateDropdown(Dropdown dropdown, int dropdownID)
    {
        dropdown.ClearOptions();

        dropdown.AddOptions(purchasedSpells.Keys.ToList<string>());

        switch(dropdownID)
        {
            case 0:
                dropdown.onValueChanged.AddListener(delegate {
                    EquipQSpell(dropdown); 
                });
                break;
            case 1:
                dropdown.onValueChanged.AddListener(delegate
                {
                    EquipESpell(dropdown);
                });
                break;
            case 2:
                dropdown.onValueChanged.AddListener(delegate
                {
                    EquipRSpell(dropdown);
                });
                break;
            case 3:
                dropdown.onValueChanged.AddListener(delegate
                {
                    EquipLSSpell(dropdown);
                });
                break;
            default:
                Debug.Log("Invalid dropdown ID");
                break;

        }
    }

    public void EquipQSpell(Dropdown change)
    {
        string key = change.options[change.value].text;
        GameObject spell = purchasedSpells[key];
        if (spell == null)
        {
            GlobalControl.Instance.QSpell = null;
            GlobalControl.Instance.QSpellIcon = null;
            QSpellIcon.color = unequip;
            QSpellIcon.sprite = null;
            return;
        }
        Sprite icon = spellIcons[key];
        GlobalControl.Instance.QSpell = spell;
        GlobalControl.Instance.QSpellIcon = icon;
        QSpellIcon.sprite = icon;
        QSpellIcon.color = Color.white;
    }

    public void EquipESpell(Dropdown change)
    {
        string key = change.options[change.value].text;
        GameObject spell = purchasedSpells[key];
        if (spell == null)
        {
            GlobalControl.Instance.ESpell = null;
            GlobalControl.Instance.ESpellIcon = null;
            ESpellIcon.color = unequip;
            ESpellIcon.sprite = null;
            return;
        }
        Sprite icon = spellIcons[key];
        ESpellIcon.color = Color.white;
        GlobalControl.Instance.ESpell = spell;
        GlobalControl.Instance.ESpellIcon = icon;
        ESpellIcon.sprite = icon;
    }

    public void EquipRSpell(Dropdown change)
    {
        string key = change.options[change.value].text;
        GameObject spell = purchasedSpells[key];
        if (spell == null)
        {
            GlobalControl.Instance.RSpell = null;
            GlobalControl.Instance.RSpellIcon = null;
            RSpellIcon.color = unequip;
            RSpellIcon.sprite = null;
            return;
        }
        Sprite icon = spellIcons[key];
        RSpellIcon.color = Color.white;
        GlobalControl.Instance.RSpell = spell;
        GlobalControl.Instance.RSpellIcon = icon;
        RSpellIcon.sprite = icon;
    }

    public void EquipLSSpell(Dropdown change)
    {
        string key = change.options[change.value].text;
        GameObject spell = purchasedSpells[key];
        if (spell == null)
        {
            GlobalControl.Instance.LSSpell = null;
            GlobalControl.Instance.LSSpellIcon = null;
            LSSpellIcon.color = unequip;
            LSSpellIcon.sprite = null;
            return;
        }
        Sprite icon = spellIcons[key];
        LSSpellIcon.color = Color.white;
        GlobalControl.Instance.LSSpell = spell;
        GlobalControl.Instance.LSSpellIcon = icon;
        LSSpellIcon.sprite = icon;
    }

    public void MoveSpeed(int cost)
    {
        GlobalControl.Instance.currentSpeed =
            GlobalControl.Instance.originalSpeed * moveSpeedIncrements[cost - 1];
        Debug.Log("Current Speed = " + GlobalControl.Instance.currentSpeed);
        UpdateShop(cost, 0);
    }

    public void FireRate(int cost)
    {
        GlobalControl.Instance.currentFireRate = 
            GlobalControl.Instance.originalFireRate * fireRateIncrements[cost - 1];
        Debug.Log("Current Fire Rate = " + GlobalControl.Instance.currentFireRate);
        UpdateShop(cost, 1);
    }

    public void AttackDamage(int cost)
    {
        GlobalControl.Instance.currentDamage =
            Mathf.CeilToInt(GlobalControl.Instance.originalDamage
            * attackDamageIncrements[cost - 1]);
        Debug.Log("Current Damage = " + GlobalControl.Instance.currentDamage);
        UpdateShop(cost, 2);
    }

    public void DamageReduction(int cost)
    {
        GlobalControl.Instance.damageReduction =
            1 - damageReductionIncrements[cost - 1];
        Debug.Log("Current Damage Reduction = " + GlobalControl.Instance.damageReduction);
        UpdateShop(cost, 3);
    }

    public void ManaRegen(int cost)
    {
        GlobalControl.Instance.currentManaRegen =
            GlobalControl.Instance.originalManaRegen + manaRegenIncrements[cost - 1];
        Debug.Log("Current Mana Regen = " + GlobalControl.Instance.currentManaRegen);
        UpdateShop(cost, 4);
    }

    public void SpellSlots(int cost)
    {
        switch (cost)
        {
            case 1:
                QSpellUnlock = true;
                break;
            case 2:
                ESpellUnlock = true;
                break;
            case 3:
                RSpellUnlock = true;
                break;
            case 4:
                LSSpellUnlock = true;
                break;
            case 5:
                Debug.Log("Something cool happens");
                break;
            default:
                Debug.Log("Invalid cost");
                break;
        }
        UpdateAllDropdowns();
        UpdateShop(cost, 5);
    }

    public void PurchaseSpell(string name)
    {
        GameObject spell = allSpells[name];
        purchasedSpells.Add(name, spell);
        LevelPointManager.purchasedSpells.Add(name);
        UpdateAllDropdowns();
        UpdateShop(spellCost, 6);
    }

    public void Exit()
    {

    }

    void UpdateShop(int cost, int upgradeId)
    {
        var levelPointManager = gameObject.GetComponent<LevelPointManager>();
        levelPointManager.levelPoints -= cost;
        switch(upgradeId)
        {
            case 0:
                levelPointManager.maxMoveSpeed++;
                break;
            case 1:
                levelPointManager.maxFireRate++;
                break;
            case 2:
                levelPointManager.maxAttackDamage++;
                break;
            case 3:
                levelPointManager.maxDamageReduction++;
                break;
            case 4:
                levelPointManager.maxManaRegen++;
                break;
            case 5:
                levelPointManager.maxSpellSlots++;
                break;
            case 6:
                Debug.Log("Purhcased a Spell");
                break;
            default:
                Debug.Log("Invalid Upgrade Id");
                break;
        }
        levelPointManager.UpdateAvailable();
        levelPointManager.UpdateLevelPointText();
    }
}
