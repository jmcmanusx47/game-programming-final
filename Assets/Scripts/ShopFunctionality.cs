using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopFunctionality : MonoBehaviour
{
    public float[] moveSpeedIncrements;
    public float[] fireRateIncrements;
    public float[] attackDamageIncrements;
    public float[] damageReductionIncrements;
    public int[] manaRegenIncrements;

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
            default:
                Debug.Log("Invalid Upgrade Id");
                break;
        }
        levelPointManager.UpdateAvailable();
        levelPointManager.UpdateLevelPointText();
    }
}
