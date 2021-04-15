using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastSpell : MonoBehaviour
{
    public string spellName;
    public float delay;
    public int cost;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpellEffect()
    {
        var effectList = gameObject.GetComponent<SpellEffects>();
        effectList.InvokeSpell(spellName, delay);
    }
}
