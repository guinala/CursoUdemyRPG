using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    private float actualHealth;
    private float maxHealth;

    // Start is called before the first frame update
    private void Update()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, actualHealth / maxHealth, 10f * Time.deltaTime);
    }

    public void ModifyHealth (float pHealth, float pMaxHealth)
    {
        actualHealth = pHealth;
        maxHealth = pMaxHealth;
    }
    
}
