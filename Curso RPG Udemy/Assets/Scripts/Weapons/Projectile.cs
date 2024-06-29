using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed;

    public CharacterAttack characterAttack { get; private set; }

    private Rigidbody2D rb;
    private Vector2 direction;
    private EnemyInteraction enemy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(enemy == null)
        {
            return;
        }
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        direction = (enemy.transform.position - transform.position);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rb.MovePosition(rb.position + direction.normalized * speed * Time.deltaTime);

    }

    public void Initialize(CharacterAttack attack)
    { 
       characterAttack = attack;
       enemy = attack.enemySelectionated;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            float damage = characterAttack.ObtainDamage();
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.AddDamage(damage);
            CharacterAttack.OnEnemyDamaged?.Invoke(damage, enemyHealth);
            gameObject.SetActive(false);
        }
    }
    
}
