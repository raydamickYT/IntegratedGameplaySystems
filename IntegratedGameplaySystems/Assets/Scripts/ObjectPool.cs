using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private GameManager manager;
    public List<ActorBase> InactivePooledObjects = new();
    public List<ActorBase> ActivePooledObjects = new();
    public ObjectPool(GameManager manager)
    {
        this.manager = manager;
    }

    //verplaatst objecten van de inactive pool naar de active pool
    public ActorBase GetPooledObjects()
    {
        //        Debug.Log(InactivePooledObjects.Count);
        if (InactivePooledObjects.Count > 0)
        {
            if (!InactivePooledObjects[0].ActiveObjectInScene.activeInHierarchy && Shooting._canFire)
            {
                ActorBase _object = InactivePooledObjects[0];
                ActivePooledObjects.Add(_object);
                InactivePooledObjects.Remove(_object);

                Debug.Log(_object.ActiveObjectInScene.activeSelf);
                return _object;
            }
        }
        else
        {
            Debug.Log("no more bullets");
        }

        return null;
    }

    public void AddObjectToPool(ActorBase item)
    {
        if (!InactivePooledObjects.Contains(item) && !ActivePooledObjects.Contains(item))
        {
            InactivePooledObjects.Add(item);
            //            Debug.Log(InactivePooledObjects.Count);
        }
    }

    //de functie die alle bullets van de active pool naar de inactive pool verplaatst.
    public void DeActivate(ActorBase _object)
    {
        if (ActivePooledObjects.Contains(_object) && _object.ActiveObjectInScene.activeInHierarchy)
        {
            Debug.Log(InactivePooledObjects.Count);
            ActivePooledObjects.Remove(_object);
            InactivePooledObjects.Add(_object);
            _object.ActiveObjectInScene.SetActive(false);
        }
        else
        {
            //Debug.Log(_object);
            //_object.ActiveObjectInScene.SetActive(false);
            AddObjectToPool(_object);
        }
    }
}