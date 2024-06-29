using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static Action<EnemyInteraction> EnemySeleccionated;
    public static Action ObjectNotSeleccionated;

    public EnemyInteraction enemyInteraction { get; set; }

    private Camera camara;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SelectionEnemy();
    }

    private void SelectionEnemy()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Enemy"));

            if (hit.collider != null)
            {
                enemyInteraction = hit.collider.GetComponent<EnemyInteraction>();
                EnemyHealth enemyHealth = enemyInteraction.GetComponent<EnemyHealth>();
                
                if(enemyHealth.health > 0)
                {
                    EnemySeleccionated?.Invoke(enemyInteraction);
                }
                else
                {
                    EnemyLoot enemyLoot = enemyInteraction.GetComponent<EnemyLoot>();
                    LootManager.Instance.DisplayLoot(enemyLoot);
                }

            }
            else
            {
                ObjectNotSeleccionated?.Invoke();
            }
        }
    }
}
