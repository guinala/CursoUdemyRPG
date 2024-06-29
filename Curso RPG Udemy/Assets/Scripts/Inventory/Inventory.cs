using BayatGames.SaveGameFree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [SerializeField] InventoryStore inventoryStore;
    [SerializeField] private Character character;
    public Character Character => character;

    [SerializeField] private int slots;
    public int Slots => slots;

    [Header("Items")]
    [SerializeField] private InventoryItem[] items;
    public InventoryItem[] itemsInventory => items;

    private readonly string KEY = "MYKEY1234567890"; 

    private void Start()
    {
        items = new InventoryItem[slots];
        SaveGame.DeleteAll();
        LoadData();
    }

    public int ObtainItemsAmount(string itemID)
    {
        List<int> indexes = VerifyStock(itemID);
        int total = 0;
        foreach(int i in indexes)
        {
            if (items[i].id == itemID)
            {
                total += items[i].Quantity;
            }
        }

        return total;
    }

    public void ConsumeItem(string ID)
    {
        List<int> indexes = VerifyStock(ID);
        if(indexes.Count > 0)
        {
            DeleteItem(indexes[indexes.Count - 1]);
        }
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

        SaveData();
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

        SaveData();
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

    private void EquipItem(int index)
    {
        if (items[index] == null)
        {
            Debug.Log("no sirve");
            return;
        }

        if (items[index].type != ItemType.Weapons)
        {
            Debug.Log("no quiero");
            return;
        }

        Debug.Log("voy a quiepar");
        items[index].Equip();
    }

    private void RemoveItem(int index)
    {
        if (items[index] == null)
        {
            return;
        }

        if (items[index].type == ItemType.Weapons)
        {
            return;
        }

        items[index].Remove();
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

        SaveData();
    }

    #region Event

    private void OnEnable()
    {
        InventorySlot.interaction += SlotResponse;
    }

    private void SlotResponse(InteractionType type, int index)
    {
        Debug.Log("voy a elegir");
        switch (type)
        {

            case InteractionType.Use:
                UseItem(index);
                break;
            case InteractionType.Equip:
                EquipItem(index);
                break;
            case InteractionType.Remove:
                RemoveItem(index);
                break;
        }
    }

    private void OnDisable()
    {
        InventorySlot.interaction -= SlotResponse;
    }

    #endregion


    #region Save/Load


    private InventoryItem ItemIsInStore(string id)
    {
        foreach(InventoryItem item in inventoryStore.Items)
        {
            if (item.id == id)
            {
                return item;
            }
        }

        return null;
    }
    
    private void SaveData()
    {
        InventoryData data = new InventoryData();
        data.ItemsData = new string[slots];
        data.ItemsAmount = new int[slots];

        for(int i = 0; i < slots; i++)
        {
            if (items[i] != null || string.IsNullOrEmpty(items[i].id))
            {
                data.ItemsData[i] = null;
                data.ItemsAmount[i] = 0;
            }

            else
            {
                data.ItemsData[i] = items[i].id;
                data.ItemsAmount[i] = items[i].Quantity;
            }

            SaveGame.Save(KEY, data);
        }
    }

    private void LoadData()
    {
        if(SaveGame.Exists(KEY))
        {
            InventoryData data = SaveGame.Load<InventoryData>(KEY);
            for(int i = 0; i < slots; i++)
            {
                if (data.ItemsData[i] != null)
                {
                    InventoryItem item = ItemIsInStore(data.ItemsData[i]);
                    if (item != null)
                    {
                        items[i] = item.CopyItem();
                        items[i].Quantity = data.ItemsAmount[i];
                        InventoryUI.Instance.drawItem(items[i], items[i].Quantity, i);
                    }
                }
                else
                {
                    items[i] = null;
                    //InventoryUI.Instance.drawItem(null, 0, i);
                }
            }
        }
    }


    #endregion
}
