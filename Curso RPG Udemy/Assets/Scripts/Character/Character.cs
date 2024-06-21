using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterStats stats;

    public CharacterExp CharacterExp { get; private set; }
    public CharacterHealth CharacterHealth { get; private set; }
    public CharacterAnimator CharacterAnimator { get; private set; }
    public CharacterMana CharacterMana { get; private set; }


    private void Awake()
    {
        CharacterHealth = GetComponent<CharacterHealth>();
        CharacterAnimator = GetComponent<CharacterAnimator>();
        CharacterMana = GetComponent<CharacterMana>();
        CharacterExp = GetComponent<CharacterExp>();
    }

    public void RestoreCharacter()
    {
        CharacterHealth.RestoreCharacter();
        CharacterAnimator.ReviveCharacter();
        CharacterMana.RestoreMana();
    }


    public void OnEnable()
    {
        ButtonAttribute.attributeEvent += AddAttribute;
    }

    public void OnDisable()
    {
        ButtonAttribute.attributeEvent -= AddAttribute;
    }

    private void AddAttribute(AttributeType attributeType)
    {
        if(stats.availablePoints <= 0)
        {
            return;
        }
        Debug.Log("si");

        switch (attributeType)
        {
            
            case AttributeType.Strength:
                //CharacterHealth.CharacterStats.strength++;
                stats.strength++;
                stats.addStrengthBonusAttribute();
                
                break;
            case AttributeType.Dexterity:
                //CharacterHealth.CharacterStats.dexterity++;
                stats.dexterity++;
                stats.addDexterityBonusAttribute();
                break;
            case AttributeType.Intelligence:
                //CharacterHealth.CharacterStats.intelligence++;
                stats.intelligence++;
                stats.addIntelligenceBonusAttribute();
                break;
        }
        stats.availablePoints--;
    }
}
