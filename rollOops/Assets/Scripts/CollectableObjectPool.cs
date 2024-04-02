using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObjectPool : MonoBehaviour
{
    public GameObject collectablePrefab;
    public int poolSize = 10;

    private Queue<GameObject> objectPool = new Queue<GameObject>();

    void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(collectablePrefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }

    public GameObject GetCollectableFromPool()
    {
        if (objectPool.Count == 0)
        {
            Debug.LogWarning("Collectable pool is empty. Consider increasing the pool size.");
            return null;
        }

        GameObject collectable = objectPool.Dequeue();
        collectable.SetActive(true);
        return collectable;
    }

    public void ReturnCollectableToPool(GameObject collectable)
    {
        collectable.SetActive(false);
        objectPool.Enqueue(collectable);
    }
}
