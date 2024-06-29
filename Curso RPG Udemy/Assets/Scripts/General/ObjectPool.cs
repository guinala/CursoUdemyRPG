using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int quantity;

    private List<GameObject> pool;
    public GameObject container { get; private set; }

    public void CreatePool(GameObject obj)
    {
        pool = new List<GameObject>();
        container = new GameObject($"Pool: {obj.name}");

        for(int i = 0; i < quantity; i++)
        {
            pool.Add(AddObject(obj));
        }
    }

    public GameObject ObtainInstance()
    {
        for(int i = 0; i < pool.Count; i++)
        {
            if (pool[i].activeSelf == false)
            {
                return pool[i];
            }
        }

        return null;
    }


    private GameObject AddObject(GameObject instance)
    {
        GameObject gameObject = Instantiate(instance, container.transform);
        gameObject.SetActive(false);

        return gameObject;
    }

    public void DestroyPool()
    {

        Destroy(container);
        pool.Clear();
    }
}
