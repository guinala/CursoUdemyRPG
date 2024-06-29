using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DetectionType
{
    Melee,
    Magic
}

public class EnemyInteraction : MonoBehaviour
{
    [SerializeField] private GameObject selectionFX;
    [SerializeField] private GameObject selectionFX_Melee;

    public void DisplaySelectedEnemy(bool state, DetectionType type)
    {
       if(type == DetectionType.Melee)
       {
           selectionFX_Melee.SetActive(state);
       }
       else
       {
           selectionFX.SetActive(state);
       }
    }


    public void DisableSelectionSprites()
    {
        selectionFX.SetActive(false);
        selectionFX_Melee.SetActive(false);
    }
}
