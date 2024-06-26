﻿using System.Collections;
using System;
using UnityEngine;


public class CharacterHealth : HealthBase
{
    public static Action DefeatedEvent;
    public bool canRestore => health < maxHealth;

    public bool Defeated { get; private set; }

    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    protected override void Start()
    {
        base.Start();
        UpdateHealthBar(health, maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            RestoreHealth(10);
        }
    }

    
    
    public void RestoreHealth(float cantidad)
    {
        if(Defeated)
        {
            return;
        }

        if (canRestore)
        {
            health += cantidad;
            if(health > maxHealth)
            {
                health = maxHealth;
            }

            UpdateHealthBar(health, maxHealth);
        }
    }

    public void RestoreCharacter()
    {
        _boxCollider2D.enabled = true;
        Defeated = false;
        health = initialHealth;
        UpdateHealthBar(health, initialHealth);
    }


    protected override void CharacterDefeated()
    {
        _boxCollider2D.enabled = false;
        Defeated = true;
        DefeatedEvent?.Invoke();

        //Esto es lo mismo que la linea de arriba
        /**
        if(DefeatedEvent != null)
        {
            DefeatedEvent.Invoke();
        }
        */
    }

    protected override void UpdateHealthBar(float actualHealth, float maxHealth)
    {
       UIManager.Instance.UpdateHealthCharacter(actualHealth, maxHealth);
    }
}
