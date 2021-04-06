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

    public AudioClip lightSFX;
    public AudioClip cloudSFX;

    int QSpellCost = 0;
    int WSpellCost = 0;
    int ESpellCost = 0;
    int RSpellCost = 0;
    int currentMana;
    Color manaColor;
    CastSpell QSpell;
    CastSpell WSpell;
    CastSpell ESpell;
    CastSpell RSpell;

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
            QSpell = QSpellSlot.GetComponent<CastSpell>();
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
            WSpell = WSpellSlot.GetComponent<CastSpell>();
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
            ESpell = ESpellSlot.GetComponent<CastSpell>();
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
            RSpell = RSpellSlot.GetComponent<CastSpell>();
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

        if (!gameObject.GetComponent<PlayerHealth>().playerDead)
        {
            if (Input.GetKeyDown("q"))
            {
                if (currentMana >= QSpellCost && QSpell != null)
                {
                    QSpell.SpellEffect();
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
                    WSpell.SpellEffect();
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
                    ESpell.SpellEffect();
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
                    RSpell.SpellEffect();
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
