using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthBase
{
    public static Action<float> EnemyLooted;

    [Header("Health")]
    [SerializeField] private EnemyHealthBar healthBarPrefab;
    [SerializeField] private Transform healthBarPosition;

    [Header("Trails")]
    [SerializeField] private GameObject trailPrefab;

    private EnemyHealthBar healthBarCreated;
    private EnemyInteraction enemyInteraction;
    private EnemyLoot enemyLoot;
    private EnemyMovement enemyMovement;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private IAController controller;

    private void Awake()
    {
        enemyInteraction = GetComponent<EnemyInteraction>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyLoot = GetComponent<EnemyLoot>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        controller = GetComponent<IAController>();
    }

    protected override void Start()
    {
        base.Start();
        CreateBar();
    }

    private void CreateBar()
    {
        healthBarCreated = Instantiate(healthBarPrefab, healthBarPosition);
        UpdateHealthBar(health, maxHealth);
    }

    protected override void CharacterDefeated()
    {
        DisableEnemy();
        EnemyLooted?.Invoke(enemyLoot.Exp);
        //Progreso Misiones
        //QuestManager.Instance.AddProgress("RookieHunter", 1);
    }

    protected override void UpdateHealthBar(float actualHealth, float maxHealth)
    {
        Debug.Log("UpdateHealthBar:"+ actualHealth + "::::" + maxHealth);
        healthBarCreated.ModifyHealth(actualHealth, maxHealth);
    }

    private void DisableEnemy()
    {
        trailPrefab.SetActive(true);
        healthBarCreated.gameObject.SetActive(false);
        enemyInteraction.DisableSelectionSprites();
        enemyMovement.enabled = false;
        spriteRenderer.enabled = false;
        boxCollider.isTrigger = true;
        controller.enabled = false;  
    }
}