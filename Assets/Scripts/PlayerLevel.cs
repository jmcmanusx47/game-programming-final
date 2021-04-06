using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    public float currentExperience = 0f;
    public int level = 0;
    public float expNeededToLevel = 100f;

    public Slider expSlider;
    public Text expText;
    void Start()
    {

        currentExperience = GlobalControl.Instance.currentExperience;
        level = GlobalControl.Instance.level;

        //currentExperience = 0f;
        //level = 0;

        expSlider.value = currentExperience;
        expText.text = currentExperience.ToString();
    }

    public void GainExperience(float xp)
    {
        Debug.Log("Gained Experience: " + xp);
        currentExperience += xp;
        GlobalControl.Instance.currentExperience = currentExperience;
        Debug.Log("Current Experience: " + currentExperience);

        if (currentExperience >= expNeededToLevel)
        {
            Debug.Log("Leveled up, Level: " + level);
            currentExperience = currentExperience % expNeededToLevel; //if we want flowover into next level, keep this.
            //currentExperience = 0;
            expNeededToLevel += 50;
            level++;
            GlobalControl.Instance.level = level;
            GlobalControl.Instance.levelPoints++;

            gameObject.GetComponent<PlayerHealth>().GainHealth(1000);
            gameObject.GetComponent<PlayerSpells>().GainMana(1000);
        }
        
        float xpPercent = (currentExperience / expNeededToLevel) * 100;
        expSlider.value = xpPercent;
        expText.text = currentExperience.ToString();
    }

    public void SavePlayerXP()
    {
        GlobalControl.Instance.currentExperience = currentExperience;
        GlobalControl.Instance.level = level;
    }
}
