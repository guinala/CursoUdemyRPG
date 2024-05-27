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
}
