using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDetector : MonoBehaviour
{
    public static Action<EnemyInteraction> EnemySeleccionated;
    public static Action EnemyLost;

    public EnemyInteraction enemyInteraction { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyInteraction = collision.GetComponent<EnemyInteraction>();

            if(enemyInteraction.GetComponent<HealthBase>().health > 0)
            {
                EnemySeleccionated?.Invoke(enemyInteraction);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyLost?.Invoke();
        }
    }
}
