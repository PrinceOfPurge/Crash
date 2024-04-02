using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour

{
    private static CollectableManager instance;
    public static CollectableManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CollectableManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("CollectableManager");
                    instance = obj.AddComponent<CollectableManager>();
                }
            }
            return instance;
        }
    }

    private HashSet<GameObject> collectables = new HashSet<GameObject>();

    public void RegisterCollectable(GameObject collectable)
    {
        collectables.Add(collectable);
    }

    public void UnregisterCollectable(GameObject collectable)
    {
        collectables.Remove(collectable);
    }

    public int GetTotalCollectables()
    {
        return collectables.Count;
    }
}

