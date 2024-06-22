using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public List<ObjectPoolData> poolData;

    private List<GameObject> pooledObjects;
    #region SingletonImplementation
    private static ObjectPoolManager instance = null;
    public static ObjectPoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ObjectPoolManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "ObjectPoolManager";
                    instance = go.AddComponent<ObjectPoolManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }
    #endregion

    private void Start()
    {
        pooledObjects = new();
        //Iterate through each PoolData
        foreach (ObjectPoolData data in poolData)
        { 
            //Instantiate the PoolData's prefab based on the initial amountToPool
            for(int i = 0; i < data.amountToPool; i++)
            {
                //Instantiate the prefab and set its parent
                GameObject obj = Instantiate(data.objectToPool, data.parent);
                //Attach the id
                obj.AddComponent<PooledItem>().id = data.id;
                //Make sure the object is disabled
                obj.SetActive(false);
                //We add it to the pool
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string id)
    {
        //Check each item from the pool
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            //Check if the object is inactive (not being used)
            //And the object has the same id as what we're looking for
            if (!pooledObjects[i].activeInHierarchy &&
                pooledObjects[i].GetComponent<PooledItem>().id == id)
            {
                return pooledObjects[i];
            }
        }
        //If id not found, return null
        return null;
    }
}
