using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFX : MonoBehaviour
{
    [SerializeField] private GameObject canvasPrefab;
    [SerializeField] private Transform canvasTransform;

    private ObjectPool objectPool;

    // Start is called before the first frame update
    private void Awake()
    {
        objectPool = GetComponent<ObjectPool>();
    }

    // Update is called once per frame
    private void Start()
    {
        objectPool.CreatePool(canvasPrefab);
    }

    private IEnumerator ShowText(float quantity)
    {
        GameObject newTextObject = objectPool.ObtainInstance();
        CanvasAnimationText canvasAnimationText = newTextObject.GetComponent<CanvasAnimationText>();
        canvasAnimationText.SetText(quantity);

        newTextObject.transform.SetParent(canvasTransform);
        newTextObject.transform.position = canvasTransform.position;
        newTextObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        newTextObject.SetActive(false);
        newTextObject.transform.SetParent(objectPool.container.transform);

    }

    private void DamageResponse(float damage)
    {
        StartCoroutine(ShowText(damage));
    }

    private void OnEnable()
    {
        IAController.OnDamageRealized += DamageResponse;
    }

    private void OnDisable()
    {
        IAController.OnDamageRealized -= DamageResponse;
    }
}
