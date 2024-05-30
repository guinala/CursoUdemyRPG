using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterHealth CharacterHealth { get; private set; }
    public CharacterAnimator CharacterAnimator { get; private set; }
    public CharacterMana CharacterMana { get; private set; }


    private void Awake()
    {
        CharacterHealth = GetComponent<CharacterHealth>();
        CharacterAnimator = GetComponent<CharacterAnimator>();
        CharacterMana = GetComponent<CharacterMana>();
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
        switch (attributeType)
        {
            case AttributeType.Strength:
                //CharacterHealth.CharacterStats.strength++;
                break;
            case AttributeType.Dexterity:
                //CharacterHealth.CharacterStats.dexterity++;
                break;
            case AttributeType.Intelligence:
                //CharacterHealth.CharacterStats.intelligence++;
                break;
        }
        //CharacterHealth.CharacterStats.availablePoints--;
    }
}
