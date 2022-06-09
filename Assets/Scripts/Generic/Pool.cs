using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pool : MonoBehaviour
{
    public bool expands = true;
    public int initialPoolSize = 10;
    public GameObject prefab;

    [SerializeField]
    private List<GameObject> pool;


    private void Start()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewPoolObject();
        }
    }

    public void CreateNewPoolObject()
    {

        if (pool == null || pool.Count <= 0)
        {
            pool = new List<GameObject>();
        }

        Debug.Log("Creating new pool object");

        Poolable newObject = Instantiate(prefab).GetComponent<Poolable>();
        newObject.gameObject.SetActive(false);
        pool.Add(newObject.gameObject);
    }

    public Poolable Spawn()
    {
        if (pool != null && pool.Count > 0)
        {
            for (int i = 0; i < pool.Count; i++)
            {
                GameObject currentGO = pool[i];
                if (!currentGO.activeInHierarchy)
                {
                    currentGO.SetActive(true);
                    Poolable currentPoolable = currentGO.GetComponent<Poolable>();
                    currentPoolable.onDespawn.AddListener(delegate
                    {
                        OnDespawn(currentPoolable);
                    });
                    currentPoolable.Spawn();
                    return currentPoolable;
                }
            }
            if (expands)
            {
                CreateNewPoolObject();
                return Spawn();
            }
        }
        else
        {
            CreateNewPoolObject();
            return Spawn();
        }
        return null;
    }

    public Poolable OnDespawn(Poolable objectToDespawn)
    {
        Debug.Log("Despawning object");
        return objectToDespawn;
    }
}