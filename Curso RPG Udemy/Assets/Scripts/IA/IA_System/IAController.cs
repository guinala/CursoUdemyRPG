using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public enum AttackTypes
{
    Melee,
    Assault
}

public class IAController : MonoBehaviour
{
    public static Action<float> OnDamageRealized;
    [Header("Stats")]
    [SerializeField] private CharacterStats stats;

    [Header("IA States")]
    [SerializeField] private IAState initialState;
    [SerializeField] private IAState defaultState;

    [Header("Configuration")]
    [SerializeField] private float detectionRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float assaulRange;
    [SerializeField] private float speedMovement;
    [SerializeField] private float assaultSpeed;
    [SerializeField] private LayerMask characterLayerMask;

    [Header("Attack")]
    [SerializeField] private float damage;
    [SerializeField] private AttackTypes attackType;
    [SerializeField] private float timeBetweenAttacks;

    [Header("Debug")]
    [SerializeField] private bool displayDetection;
    [SerializeField] private bool displayAttackRange;
    [SerializeField] private bool displayAssaultRange;

    public Transform CharacterReference { get; set; }
    public IAState currentState { get; set; }
    public EnemyMovement EnemyMovement { get; set; }
    public float DetectionRange => detectionRange;
    public float Damage => damage;
    public AttackTypes AttackType => attackType;
    public float Speed => speedMovement;
    public LayerMask CharacterLayerMask => characterLayerMask;

    private float timeToNextAttack;
    private BoxCollider2D boxCollider2D;
    public float RangeDetermined => attackType == AttackTypes.Assault ? assaulRange : attackRange;

    private void Start()
    {
        currentState = initialState;
        EnemyMovement = GetComponent<EnemyMovement>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        currentState.ExecuteState(this);
    }

    public void changeState(IAState state)
    {
        if(state != defaultState)
        {
            currentState = state;
        }
    }

    public bool IsCharacterInRange(float range)
    {
        float distance = (CharacterReference.position - transform.position).sqrMagnitude;

        if(distance < Mathf.Pow(range, 2))
        {
            return true;
        }

        return false;
    }

    public void MeleeAttack(float quantity)
    {
        if(CharacterReference == null)
        {
            return;
        }

        ApplyDamageToCharacter(quantity);
    }

    public void ApplyDamageToCharacter(float quantity)
    {
        float damageToRealize = 0;

        if(Random.value < (stats.Block / 100))
        {
            return;
        }
        damageToRealize = Mathf.Max(quantity- stats.Defense, 1);

        CharacterReference.GetComponent<CharacterHealth>().AddDamage(damageToRealize);
        OnDamageRealized?.Invoke(damageToRealize);
    }

    public void Assault(float quantity)
    {
        StartCoroutine(IEAssault(quantity));
    }   

    IEnumerator IEAssault(float quantity)
    {
        Vector3 CharacterPosition = CharacterReference.position;
        Vector3 initialPosition = transform.position;
        Vector3 directionToCharacter = (CharacterPosition - initialPosition).normalized;
        Vector3 AttackPosition = CharacterPosition - directionToCharacter * 0.5f;

        float attackTransition = 0f;

        boxCollider2D.enabled = false;
        while(attackTransition < 1f)
        {
            attackTransition += Time.deltaTime * assaultSpeed;
            float interpolacion = (-Mathf.Pow(attackTransition, 2) + attackTransition) * 4f;
            transform.position = Vector3.Lerp(initialPosition, AttackPosition, interpolacion);  
            yield return null;
        }

        if(CharacterReference != null)
        {
            ApplyDamageToCharacter(quantity);
        }

        boxCollider2D.enabled = true;

    }


    public bool IsTimeToAttack()
    {
        if(Time.time >= timeToNextAttack)
        {
            return true;
        }
        return false;
    }

    public void UpdateAttackTimeBetweenAttacks()
    {
        timeToNextAttack = Time.time + timeBetweenAttacks;
    }
    private void OnDrawGizmos()
    {
        if(displayDetection)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }

        if(displayAttackRange)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }

        if(displayAssaultRange)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, assaulRange);
        }
    }
}
