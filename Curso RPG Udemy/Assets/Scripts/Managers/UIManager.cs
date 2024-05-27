using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    

    [Header("Bars)")]
    [SerializeField] private Image healthImage;
    [SerializeField] private Image manaImage;
    [SerializeField] private Image expImage;

    [Header("Text)")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private TextMeshProUGUI expText;

    private float actualHealth;
    private float actualMana;
    private float actualExp;
    private float maxHealth;
    private float maxMana;
    private float expNextlevel;

    // Update is called once per frame
    void Update()
    {
        UpdateUICharacter();    
    }

    private void UpdateUICharacter()
    {
        healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, actualHealth / maxHealth, Time.deltaTime * 10);

        //String interpolation
        healthText.text = $"{actualHealth} / {maxHealth}";

        manaImage.fillAmount = Mathf.Lerp(manaImage.fillAmount, actualMana / maxMana, Time.deltaTime * 10);
        manaText.text = $"{actualMana} / {maxMana}";

        expImage.fillAmount = Mathf.Lerp(expImage.fillAmount, actualExp / expNextlevel, Time.deltaTime * 10);
        expText.text = $"{((actualExp/expNextlevel)*100):F2}%";
    }

    public void UpdateHealthCharacter(float pActualHealth, float pMaxHealth)
    {
        actualHealth = pActualHealth;
        maxHealth = pMaxHealth;

        //healthImage.fillAmount = actualHealth / maxHealth;
        //healthText.text = actualHealth + " / " + maxHealth;
    }

    public void UpdateManaCharacter(float pActualMana, float pMaxMana)
    {
        actualMana = pActualMana;
        maxMana = pMaxMana;

        //healthImage.fillAmount = actualHealth / maxHealth;
        //healthText.text = actualHealth + " / " + maxHealth;
    }

    public void UpdateExpCharacter(float pActualExp, float pExpRequired)
    {
        actualExp = pActualExp;
        expNextlevel = pExpRequired;

        //healthImage.fillAmount = actualHealth / maxHealth;
        //healthText.text = actualHealth + " / " + maxHealth;
    }
}
