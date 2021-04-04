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
    public int maxMoveSpeed = 0;
    public int maxFireRate = 0;
    public int maxAttackDamage = 0;
    public int maxDamageReduction = 0;
    public int maxManaRegen = 0;
    public int maxSpellSlots = 0;
    public Text levelPointText;
    // Start is called before the first frame update
    void Start()
    {
        levelPoints = GlobalControl.Instance.levelPoints;
        UpdateLevelPointText();
        UpdateAvailable();
    }

    public void UpdateAvailable()
    {
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

    public void UpdateLevelPointText()
    {
        levelPointText.text = "" + levelPoints;
    }
}
