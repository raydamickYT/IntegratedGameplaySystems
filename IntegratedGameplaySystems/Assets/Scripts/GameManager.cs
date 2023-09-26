using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Adjustable Variables
    public int AmountToPool = 30;
    #endregion

    #region Delegates
    public delegate void Deactivationhandler(GameObject bullet);
    public Deactivationhandler DeactivationDelegate;
    public delegate GameObject ObjectPoolDelegate();
    public ObjectPoolDelegate objectPoolDelegate;
    #endregion

    public InputHandler inputHandler;
    public ObjectPool ObjectPool;
    public GameObject Prefab;

    #region Dictionaries and Lists
    public Dictionary<string, GameObject> PrefabLibrary = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> InstantiatedObjects = new Dictionary<string, GameObject>();
    #endregion

    private void Start()
    {
        ObjectPool = new ObjectPool();

        for (int i = 0; i < AmountToPool; i++)
        {
            GameObject instantiatedObject = Instantiate(Prefab);
            instantiatedObject.SetActive(false);
            ObjectPool.AddObjectToPool(instantiatedObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject gameObject = ObjectPool?.GetPooledObjects();
            gameObject.transform.position = transform.position;
        }
    }
}
