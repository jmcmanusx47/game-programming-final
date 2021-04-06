using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
<<<<<<< Updated upstream
=======
    public Image QSpellIcon;
    public Image WSpellIcon;
    public Image ESpellIcon;
    public Image RSpellIcon;
    public Image blankSpellIcon;
    public Color unequip;
>>>>>>> Stashed changes

    public AudioClip lightSFX;
    public AudioClip cloudSFX;

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
            QSpell.player = gameObject;
            QManaCostText.text = QSpellCost.ToString();
<<<<<<< Updated upstream
=======
            QSpellIcon.sprite = GlobalControl.Instance.QSpellIcon;
            QSpellIcon.color = Color.white;
>>>>>>> Stashed changes
        }
        else
        {
            QManaCostText.text = "";
<<<<<<< Updated upstream
=======
            QSpellIcon.sprite = null;
            QSpellIcon.color = unequip;
>>>>>>> Stashed changes
        }

        if (WSpellSlot != null)
        {
            WSpell = WSpellSlot.GetComponent<SpellEffects>();
            WSpellCost = WSpell.cost;
            WSpell.player = gameObject;
            WManaCostText.text = WSpellCost.ToString();
<<<<<<< Updated upstream
=======
            WSpellIcon.sprite = GlobalControl.Instance.ESpellIcon;
            WSpellIcon.color = Color.white;
>>>>>>> Stashed changes
        }
        else
        {
            WManaCostText.text = "";
<<<<<<< Updated upstream
=======
            WSpellIcon.sprite = null;
            WSpellIcon.color = unequip;
>>>>>>> Stashed changes
        }

        if (ESpellSlot != null)
        {
            ESpell = ESpellSlot.GetComponent<SpellEffects>();
            ESpellCost = ESpell.cost;
            ESpell.player = gameObject;
            EManaCostText.text = ESpellCost.ToString();
<<<<<<< Updated upstream
=======
            ESpellIcon.sprite = GlobalControl.Instance.RSpellIcon;
            ESpellIcon.color = Color.white;
>>>>>>> Stashed changes
        }
        else
        {
            EManaCostText.text = "";
<<<<<<< Updated upstream
=======
            ESpellIcon.sprite = null;
            ESpellIcon.color = unequip;
>>>>>>> Stashed changes
        }

        if (RSpellSlot != null)
        {
            RSpell = RSpellSlot.GetComponent<SpellEffects>();
            RSpellCost = RSpell.cost;
            RSpell.player = gameObject;
            RManaCostText.text = RSpellCost.ToString();
<<<<<<< Updated upstream
=======
            RSpellIcon.sprite = GlobalControl.Instance.LSSpellIcon;
            RSpellIcon.color = Color.white;
>>>>>>> Stashed changes
        }
        else
        {
            RManaCostText.text = "";
<<<<<<< Updated upstream
=======
            RSpellIcon.sprite = null;
            RSpellIcon.color = unequip;
>>>>>>> Stashed changes
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpellColors();

        if (!gameObject.GetComponent<PlayerHealth>().playerDead)
        {
            if (Input.GetKeyDown("q"))
            {
                if (currentMana >= QSpellCost && QSpell != null)
                {
                    QSpell.InvokeSpell(gameObject);
                    LoseMana(QSpellCost);
                }
                else
                {
                    // Spell Fizzle SFX
                }
            }
            else if (Input.GetKeyDown("e"))
            {
                if (currentMana >= WSpellCost && WSpell != null)
                {
                    WSpell.InvokeSpell(gameObject);
                    AudioSource.PlayClipAtPoint(cloudSFX, transform.position);
                    LoseMana(WSpellCost);
                }
                else
                {
                    // Spell Fizzle SFX
                }
            }
            else if (Input.GetKeyDown("r"))
            {
                if (currentMana >= ESpellCost && ESpell != null)
                {
                    ESpell.InvokeSpell(gameObject);
                    AudioSource.PlayClipAtPoint(lightSFX, transform.position);
                    LoseMana(ESpellCost);
                }
                else
                {
                    // Spell Fizzle SFX
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (currentMana >= RSpellCost && RSpell != null)
                {
                    RSpell.InvokeSpell(gameObject);
                    LoseMana(RSpellCost);
                }
                else
                {
                    // Spell Fizzle SFX
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
