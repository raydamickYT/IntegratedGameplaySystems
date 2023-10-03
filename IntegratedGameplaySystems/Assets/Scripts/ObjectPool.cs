using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public List<GameObject> InactivePooledObjects = new();
    public List<GameObject> ActivePooledObjects = new();

    public void AddObjectToPool(GameObject item)
    {
        if (!InactivePooledObjects.Contains(item) && !ActivePooledObjects.Contains(item))
        {
            InactivePooledObjects.Add(item);
        }
    }

    //verplaatst objecten van de inactive pool naar de active pool
    public GameObject GetPooledObjects()
    {
        Debug.Log(InactivePooledObjects.Count);
        if (InactivePooledObjects.Count > 0)
        {
            if (!InactivePooledObjects[0].activeInHierarchy && FireGunCommand._canFire)
            {
                GameObject _object = InactivePooledObjects[0];
                ActivePooledObjects.Add(_object);
                InactivePooledObjects.Remove(_object);
                _object.SetActive(true);
                return _object;
            }
        }
        else
        {
            Debug.Log("no more bullets");
        }

        return null;
    }

    //de functie die alle bullets van de active pool naar de inactive pool verplaatst.
    public void DeActivate(GameObject _object)
    {
        if (ActivePooledObjects.Contains(_object))
        {
            ActivePooledObjects.Remove(_object);
            InactivePooledObjects.Add(_object);
            _object.SetActive(false);
        }
    }
}