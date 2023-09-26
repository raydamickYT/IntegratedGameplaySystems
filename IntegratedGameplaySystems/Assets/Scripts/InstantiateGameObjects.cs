using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

//scirpt voor het maken van de game objecten
public class InstantiateGameObjects : State<GameManager>
{
    ObjectPool objectPool = new ObjectPool();
    protected FSM<GameManager> owner;
    //dependency injection (dit geval met de gamemanager monobehaviour)
    public InstantiateGameObjects(FSM<GameManager> _owner)
    {
        owner = _owner;
    }

    //dit is hetzelfde als een awakefunctie en wordt aangeroepen vannuit de StateMachine.
    //omdat deze class alleen geroepen wordt aan het begin wanneer de game opstart, heb ik nu alleen de OnEnter method hier.
    public override void OnEnter()
    {
        //voeg gameobjecten toe aan je dictionary
        owner.pOwner.PrefabLibrary.Add("player", owner.pOwner.Prefab);
        //manager.PrefabLibrary.Add("Bullet", manager.PreFab);

        //voeg bullets toe aan de dictionary
        for (int i = 0; i < owner.pOwner.AmountToPool; i++)
        {
            //owner.pOwner.PrefabLibrary.Add("Bullet" + i.ToString(), owner.pOwner.bullets.BulletObject);
        }
        //manager.PrefabLibrary.Add("enemy", manager.PreFab);

        //instantiaten van alle objecten in de dictionary
        //kvp staat voor keyvaluepair you stoopid
        foreach (var kvp in owner.pOwner.PrefabLibrary)
        {
            Vector3 startPos = new Vector3(0, 0, 0);
            //check welk object er gespawned wordt en verander de positie afhankelijk van andere objecten
            if (kvp.Key == "player")
            {
                startPos = new Vector3(0, -4.4f, 0);
            }
            if (kvp.Key.StartsWith("Bullet"))
            {
                GameObject test = owner.pOwner.InstantiatedObjects["player"];
                startPos = test.transform.position;
            }

            GameObject instantiatedObject = GameObject.Instantiate(kvp.Value, startPos, Quaternion.identity);
            instantiatedObject.name = kvp.Key; // optioneel, maar netjes: verander de naam van het gameobject in de wereld naar de kvp waarde(string)

            //doe wat logic om de bullets inactive te maken en toe te voegen aan de object pool.
            if (kvp.Key.StartsWith("Bullet"))
            {
                instantiatedObject.SetActive(false);
                //owner.pOwner.InactivePooledObjects.Add(instantiatedObject);
            }

            //hier de instantiated object toevoegden aan de library
            owner.pOwner.InstantiatedObjects.Add(kvp.Key, instantiatedObject);
        }
        owner.SwitchState(typeof(IdleState));
    }
}

public class IdleState : State<GameManager>
{
    public FSM<GameManager> owner;

    public IdleState(FSM<GameManager> _owner)
    {
        owner = _owner;
    }

    //dit is de idle state, doe hier iets als de speler niets doet.
}
