using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyLoot : MonoBehaviour
{
    

    [Header("Exp")]
    [SerializeField] private float exp;

    [Header("Loot")]
    [SerializeField] private DropItem[] dropItems;


    private List<DropItem> droppedItems = new List<DropItem>();
    public List<DropItem> SelectedItems => droppedItems;
    public float Exp => exp;

    private void Start()
    {
        SelectLoot();
    }

    private void SelectLoot()
    {
        foreach(DropItem dropItem in dropItems)
        {
            float randomChance = Random.Range(0, 100);

            if(randomChance <= dropItem.dropChance)
            {
                droppedItems.Add(dropItem);
            }
        }
    }
}
