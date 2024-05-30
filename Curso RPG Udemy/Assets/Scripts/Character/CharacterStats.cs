using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Character/Stats")]
public class CharacterStats : ScriptableObject
{
    [Header("Stats")]
    public float Damage = 5f;
    public float Level;
    public float Defense = 2f;
    public float Speed = 5f;
    public float Experience;
    public float ExpRequired;
    [Range(0, 100f)] public float Critical;
    [Range(0, 100f)] public float Block;

    [Header("Attributes")]
    public int strength;
    public int dexterity;
    public int intelligence;

    [HideInInspector] public int availablePoints;

    public void ResetStats()
    {
        Damage = 5f;
        Level = 1;
        Defense = 2f;
        Speed = 5f;
        Experience = 0f;
        ExpRequired = 0f;
        Critical = 0f;
        Block = 0f;

        strength = 0;
        dexterity = 0;
        intelligence = 0;

        availablePoints = 0;
    }
}
