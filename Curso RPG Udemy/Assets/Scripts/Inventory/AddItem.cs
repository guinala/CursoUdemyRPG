using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AddItem : MonoBehaviour
{
   [Header("Config")]
   [SerializeField] private InventoryItem itemReference;
   [SerializeField] private int addQuantity;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hola");
        if (other.CompareTag("Player"))
        {
            Inventory.Instance.AddItem(itemReference, addQuantity);
            Destroy(gameObject);
            Debug.Log("si");
        }
    }
    
}
