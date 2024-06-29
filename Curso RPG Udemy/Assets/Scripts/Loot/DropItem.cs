using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class DropItem 
{
    [Header("Info")]
    public string Name;
    public InventoryItem Item;
    public int amount;

    [Header("Drop Chance")]
    [Range(0, 100)] public float dropChance;

    public bool Looted { get; set; }
}
