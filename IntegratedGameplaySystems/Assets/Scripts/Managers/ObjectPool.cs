using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public List<ActorBase> InactivePooledObjects = new();
    public List<ActorBase> ActivePooledObjects = new();

    public ObjectPool(GameManager gameManager)
    {
        gameManager.OnDisableEvent += OnDisable;
    }

    private void OnDisable()
    {
        foreach (var bullet in InactivePooledObjects)
        {
            if (bullet.ActiveObjectInScene != null)
            {
                GameObject.Destroy(bullet.ActiveObjectInScene);
            }
        }
        foreach (var bullet in ActivePooledObjects)
        {
            if (bullet.ActiveObjectInScene != null)
            {
                GameObject.Destroy(bullet.ActiveObjectInScene);
            }
        }
        InactivePooledObjects.Clear();
        ActivePooledObjects.Clear();
    }

    public ActorBase GetPooledObjects()
    {
        if (InactivePooledObjects.Count > 0)
        {
            if (!InactivePooledObjects[0].ActiveObjectInScene.activeInHierarchy && Shooting._canFire)
            {
                ActorBase _object = InactivePooledObjects[0];
                ActivePooledObjects.Add(_object);
                InactivePooledObjects.Remove(_object);

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
        }
    }

    public void DeActivate(ActorBase _object)
    {
        if (ActivePooledObjects.Contains(_object) && _object.ActiveObjectInScene.activeInHierarchy)
        {
            ActivePooledObjects.Remove(_object);
            InactivePooledObjects.Add(_object);
            _object.ActiveObjectInScene.SetActive(false);
        }
        else
        {
            AddObjectToPool(_object);
        }
    }

}