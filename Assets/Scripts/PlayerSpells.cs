using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSpells : MonoBehaviour
{
    public GameObject QSpellSlot;
    public GameObject WSpellSlot;
    public GameObject ESpellSlot;
    public GameObject RSpellSlot;
    public int startingMana = 100;
    public Slider manaSlider;
    public Text manaText;
    public Text QManaCostText;
    public Text WManaCostText;
    public Text EManaCostText;
    public Text RManaCostText;
    public Image QSpellIcon;
    public Image WSpellIcon;
    public Image ESpellIcon;
    public Image RSpellIcon;
    public Image blankSpellIcon;
    public Color unequip;
    public bool buff;
    public bool questionUpgrade;
    public AudioClip emptySFX;

    int QSpellCost = 0;
    int WSpellCost = 0;
    int ESpellCost = 0;
    int RSpellCost = 0;
    int currentMana;
    Color manaColor;
    SpellEffects QSpell;
    SpellEffects WSpell;
    SpellEffects ESpell;
    SpellEffects RSpell;

    // Start is called before the first frame update
    void Start()
    {
        QSpellSlot = GlobalControl.Instance.QSpell;
        WSpellSlot = GlobalControl.Instance.ESpell;
        ESpellSlot = GlobalControl.Instance.RSpell;
        RSpellSlot = GlobalControl.Instance.LSSpell;
        questionUpgrade = GlobalControl.Instance.questionUpgrade;

        currentMana = startingMana;
        manaSlider.value = currentMana;
        manaText.text = currentMana.ToString();
        manaColor = manaText.color;

        SpellInitialization();
    }

    void SpellInitialization()
    {
        if (QSpellSlot != null)
        {
            QSpell = QSpellSlot.GetComponent<SpellEffects>();
            QSpellCost = QSpell.cost;
            QManaCostText.text = QSpellCost.ToString();
            QSpellIcon.sprite = GlobalControl.Instance.QSpellIcon;
            QSpellIcon.color = Color.white;
        }
        else
        {
            QManaCostText.text = "";
            QSpellIcon.sprite = null;
            QSpellIcon.color = unequip;
        }

        if (WSpellSlot != null)
        {
            WSpell = WSpellSlot.GetComponent<SpellEffects>();
            WSpellCost = WSpell.cost;
            WManaCostText.text = WSpellCost.ToString();
            WSpellIcon.sprite = GlobalControl.Instance.ESpellIcon;
            WSpellIcon.color = Color.white;
        }
        else
        {
            WManaCostText.text = "";
            WSpellIcon.sprite = null;
            WSpellIcon.color = unequip;
        }

        if (ESpellSlot != null)
        {
            ESpell = ESpellSlot.GetComponent<SpellEffects>();
            ESpellCost = ESpell.cost;
            EManaCostText.text = ESpellCost.ToString();
            ESpellIcon.sprite = GlobalControl.Instance.RSpellIcon;
            ESpellIcon.color = Color.white;
        }
        else
        {
            EManaCostText.text = "";
            ESpellIcon.sprite = null;
            ESpellIcon.color = unequip;
        }

        if (RSpellSlot != null)
        {
            RSpell = RSpellSlot.GetComponent<SpellEffects>();
            RSpellCost = RSpell.cost;
            RManaCostText.text = RSpellCost.ToString();
            RSpellIcon.sprite = GlobalControl.Instance.LSSpellIcon;
            RSpellIcon.color = Color.white;
        }
        else
        {
            RManaCostText.text = "";
            RSpellIcon.sprite = null;
            RSpellIcon.color = unequip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpellColors();

        if (!gameObject.GetComponent<PlayerHealth>().playerDead &&
            (!buff || questionUpgrade))
        {
            if (Input.GetKeyDown("q"))
            {
                if (currentMana >= QSpellCost && QSpell != null)
                {
                    QSpell.InvokeSpell();
                    LoseMana(QSpellCost);
                }
                else
                {
                    // Spell Fizzle SFX
                    AudioSource.PlayClipAtPoint(emptySFX, Camera.main.transform.position);
                }
            }
            else if (Input.GetKeyDown("e"))
            {
                if (currentMana >= WSpellCost && WSpell != null)
                {
                    WSpell.InvokeSpell();
                    LoseMana(WSpellCost);
                }
                else
                {
                    // Spell Fizzle SFX
                    AudioSource.PlayClipAtPoint(emptySFX, Camera.main.transform.position);
                }
            }
            else if (Input.GetKeyDown("r"))
            {
                if (currentMana >= ESpellCost && ESpell != null)
                {
                    ESpell.InvokeSpell();
                    LoseMana(ESpellCost);
                }
                else
                {
                    // Spell Fizzle SFX
                    AudioSource.PlayClipAtPoint(emptySFX, Camera.main.transform.position);
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (currentMana >= RSpellCost && RSpell != null)
                {
                    RSpell.InvokeSpell();
                    LoseMana(RSpellCost);
                }
                else
                {
                    // Spell Fizzle SFX
                    AudioSource.PlayClipAtPoint(emptySFX, Camera.main.transform.position);
                }
            }
        }
    }

    public void GainMana(int manaRegen)
    {
        currentMana += manaRegen;
        currentMana = Mathf.Clamp(currentMana, 0, startingMana);
        manaSlider.value = currentMana;
        manaText.text = currentMana.ToString();
    }

    public void LoseMana(int cost)
    {
        currentMana -= cost;
        currentMana = Mathf.Clamp(currentMana, 0, startingMana);
        manaSlider.value = currentMana;
        manaText.text = currentMana.ToString();
    }

    private void UpdateSpellColors()
    {
        if (currentMana < QSpellCost)
        {
            QManaCostText.color = Color.gray;
        }
        else
        {
            QManaCostText.color = manaColor;
        }

        if (currentMana < WSpellCost)
        {
            WManaCostText.color = Color.gray;
        }
        else
        {
            WManaCostText.color = manaColor;
        }

        if (currentMana < ESpellCost)
        {
            EManaCostText.color = Color.gray;
        }
        else
        {
            EManaCostText.color = manaColor;
        }

        if (currentMana < RSpellCost)
        {
            RManaCostText.color = Color.gray;
        }
        else
        {
            RManaCostText.color = manaColor;
        }

    }
}
