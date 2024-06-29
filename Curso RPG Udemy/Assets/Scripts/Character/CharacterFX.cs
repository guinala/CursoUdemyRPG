using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Player, IA
}


public class CharacterFX : MonoBehaviour
{
    [SerializeField] private GameObject canvasPrefab;
    [SerializeField] private Transform canvasTransform;

    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private CharacterType characterType;

    private EnemyHealth enemyHealth;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    private void Start()
    {
        objectPool.CreatePool(canvasPrefab);
    }

    private IEnumerator ShowText(float quantity, Color color)
    {
        GameObject newTextObject = objectPool.ObtainInstance();
        CanvasAnimationText canvasAnimationText = newTextObject.GetComponent<CanvasAnimationText>();
        canvasAnimationText.SetText(quantity, color);

        newTextObject.transform.SetParent(canvasTransform);
        newTextObject.transform.position = canvasTransform.position;
        newTextObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        newTextObject.SetActive(false);
        newTextObject.transform.SetParent(objectPool.container.transform);

    }

    private void DamageResponsePlayer(float damage)
    {
        if(characterType == CharacterType.Player)
        {
            StartCoroutine(ShowText(damage, Color.black));
        }
    }

    private void DamageResponseEnemy(float damage, EnemyHealth health)
    {
        if(characterType == CharacterType.IA && enemyHealth == health)
        {
            StartCoroutine(ShowText(damage, Color.red));
        }
    }


    private void OnEnable()
    {
        IAController.OnDamageRealized += DamageResponsePlayer;
        CharacterAttack.OnEnemyDamaged += DamageResponseEnemy;
    }

    private void OnDisable()
    {
        IAController.OnDamageRealized -= DamageResponsePlayer;
        CharacterAttack.OnEnemyDamaged -= DamageResponseEnemy;
    }
}
