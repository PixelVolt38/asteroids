using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{

    public static BulletPooling SharedInstance;

    public List<GameObject> pooledObjects;

    public GameObject objectToPool;

    public int amountToPool;

    private void Awake()
    {
        SharedInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject newObject;

        for (int i = 0; i < amountToPool; i++)
        {
            newObject = Instantiate(objectToPool);
            newObject.SetActive(false);
            pooledObjects.Add(newObject);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }

        }
        
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
