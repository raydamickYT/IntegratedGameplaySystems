using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ObjectPool
{
    private GameManager manager;

    public ObjectPool(GameManager manager)
    {
        this.manager = manager;
    }

    //verplaatst objecten van de inactive pool naar de active pool
    public GameObject GetPooledObjects()
    {
        if (manager.InactivePooledObjects.Count > 0)
        {
            if (!manager.InactivePooledObjects[0].activeInHierarchy && FireGunCommand._canFire)
            {
                GameObject _object = manager.InactivePooledObjects[0];
                manager.ActivePooledObjects.Add(_object);
                manager.InactivePooledObjects.Remove(_object);
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
    public void DeActivate(GameObject bullet)
    {
        if (manager.ActivePooledObjects.Contains(bullet))
        {
            manager.ActivePooledObjects.Remove(bullet);
            manager.InactivePooledObjects.Add(bullet);
            bullet.SetActive(false);
        }
    }
}