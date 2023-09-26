using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    FSM<GameManager> fsm;
    #region Adjustable Variables
    public int AmountToPool = 30;
    #endregion

    #region Delegates
    //Delegates
    public delegate void Deactivationhandler(GameObject bullet);
    public Deactivationhandler DeactivationDelegate;
    public delegate GameObject ObjectPoolDelegate();
    public ObjectPoolDelegate objectPoolDelegate;
    #endregion

    public InputHandler inputHandler;
    public ObjectPool objectPool;
    public GameObject PreFab;
    //scriptable object
    #region Dictionaries and Lists
    public Dictionary<string, GameObject> PrefabLibrary = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> InstantiatedObjects = new Dictionary<string, GameObject>();

    //alle dictionaries voor mijn simpele object pool
    public List<GameObject> InactivePooledObjects = new List<GameObject>();
    public List<GameObject> ActivePooledObjects = new List<GameObject>();
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        var fireGun = new FireGunCommand(fsm);
        inputHandler.BindInputToCommand(KeyCode.X, fireGun, new MovementContext { Direction = Vector3.up });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
