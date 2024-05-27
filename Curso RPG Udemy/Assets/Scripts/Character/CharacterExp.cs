using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExp : MonoBehaviour
{

    [SerializeField] private int maxLevel;
    [SerializeField] private int expbase; //Experiencia base requerida para subir de nivel
    [SerializeField] private int expIncrement;

    public int Level { get; set; }

    private float expTempActual;
    private float expRequired;

    // Start is called before the first frame update
    void Start()
    {
        Level = 1;
        expRequired = expbase;

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
                UpdateLevel();
                AddExp(expObtained);

            }

            else
            {
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
        if(Level < maxLevel)
        {
            Level++;
            expTempActual = 0;
            expRequired += expIncrement;
        }
    }

    private void UpdateExpBar()
    {
        UIManager.Instance.UpdateExpCharacter(expTempActual, expRequired);
    }

}