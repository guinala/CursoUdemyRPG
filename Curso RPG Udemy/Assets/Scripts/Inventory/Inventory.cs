using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [SerializeField] private Character character;
    public Character Character => character;

    [SerializeField] private int slots;
    public int Slots => slots;

    [Header("Items")]
    [SerializeField] private InventoryItem[] items;
    public InventoryItem[] itemsInventory => items;

    private void Start()
    {
        items = new InventoryItem[slots];
    }

    public void AddItem(InventoryItem itemReference, int addQuantity)
    {
        if(itemReference == null)
        {
            return;
        }

        //Hay ya otro item similar en inventario?
        List<int> itemIndexes = VerifyStock(itemReference.id);

        if (itemReference.Stackable)
        {
            if (itemIndexes.Count > 0)
            {
                for(int i = 0; i < itemIndexes.Count; i++)
                {
                    if (items[itemIndexes[i]].Quantity < itemReference.maxAccumulation)
                    {
                        items[itemIndexes[i]].Quantity += addQuantity;
                        if (items[itemIndexes[i]].Quantity > itemReference.maxAccumulation)
                        {
                            int dif = items[itemIndexes[i]].Quantity - itemReference.maxAccumulation;
                            items[itemIndexes[i]].Quantity = itemReference.maxAccumulation;
                            AddItem(itemReference, dif);
                        }

                        InventoryUI.Instance.drawItem(itemReference, items[itemIndexes[i]].Quantity, itemIndexes[i]);
                        return;
                    }
                }
            }
        }

        if(addQuantity <= 0)
        {
            return;
        }

        if(addQuantity > itemReference.maxAccumulation)
        {
           AddItemInSlot(itemReference, itemReference.maxAccumulation);
           addQuantity -= itemReference.maxAccumulation;
           AddItem(itemReference, addQuantity);

        }

        else
        {
            AddItemInSlot(itemReference, addQuantity);
        }
    }

    private List<int> VerifyStock(string id)
    {
        List<int> itemIndexes = new List<int>();
        
        for(int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                if (items[i].id == id)
                {
                    itemIndexes.Add(i);
                }
            }
            
        }


        return itemIndexes;
    }

    private void AddItemInSlot(InventoryItem item, int cantidad)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = item.CopyItem();
                items[i].Quantity = cantidad;
                InventoryUI.Instance.drawItem(item, cantidad, i);
                return;
            }
        }
    }

    private void DeleteItem(int index)
    {
        items[index].Quantity--;

        if (items[index].Quantity <= 0)
        {
            items[index].Quantity = 0;
            items[index] = null;
            InventoryUI.Instance.drawItem(null, 0, index);
        }

        else
        {
            InventoryUI.Instance.drawItem(items[index], items[index].Quantity, index);
        }
    }

    private void UseItem(int index)
    {
        if (items[index] == null)
        {
            return;
        }

        if (items[index].Use())
        {
            DeleteItem(index);
        }
    }

    public void MoveItem(int initialIndex, int finalIndex)
    {
        if (items[initialIndex] == null || items[finalIndex] != null)
        {
            return;
        }

        //Copy items in final slot
        InventoryItem item = items[initialIndex].CopyItem();
        items[finalIndex] = item;
        InventoryUI.Instance.drawItem(item, item.Quantity, finalIndex);

        //Delete item in initial slot
        items[initialIndex] = null;
        InventoryUI.Instance.drawItem(null, 0, initialIndex);
    }

    #region Event

    private void OnEnable()
    {
        InventorySlot.interaction += SlotResponse;
    }

    private void SlotResponse(InteractionType type, int index)
    {
        switch (type)
        {
            case InteractionType.Use:
                UseItem(index);
                break;
            case InteractionType.Equip:
                break;
            case InteractionType.Remove:
                break;
        }
    }

    private void OnDisable()
    {
        InventorySlot.interaction -= SlotResponse;
    }

    #endregion



}
