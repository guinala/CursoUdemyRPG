using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExp : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private CharacterStats stats;


    [Header("Config")]
    [SerializeField] private int maxLevel;
    [SerializeField] private int expbase; //Experiencia base requerida para subir de nivel
    [SerializeField] private int expIncrement;

    private float actualExp;
    private float expTempActual;
    private float expRequired;

    // Start is called before the first frame update
    void Start()
    {
        stats.Level = 1;
        expRequired = expbase;
        stats.ExpRequired = expRequired;

        UpdateExpBar();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            AddExp(2);
        }
    }

    public void AddExp(float expObtained)
    {
        if (expObtained > 0f)
        {
            float expRestant = expRequired - expTempActual;
            if (expObtained > expRestant)
            {
                expObtained -= expRestant;
                actualExp += expObtained;
                UpdateLevel();
                AddExp(expObtained);

            }

            else
            {
                actualExp += expObtained;
                expTempActual += expObtained;
                if(expTempActual == expRequired)
                {
                    UpdateLevel();
                }
            }

        }

        UpdateExpBar();
    }

    private void UpdateLevel()
    {
        if(stats.Level < maxLevel)
        {
            stats.Level++;
            expTempActual = 0;
            expRequired *= expIncrement;
            stats.ExpRequired = expRequired;
            stats.availablePoints += 3;
        }
    }

    private void UpdateExpBar()
    {
        UIManager.Instance.UpdateExpCharacter(expTempActual, expRequired);
    }

}