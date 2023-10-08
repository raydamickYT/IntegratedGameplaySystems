using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private readonly FSM<GameManager> fsm = new();

    public ActorData playerData;

    #region Delegates
    public delegate void Deactivationhandler(ActorBase bullet);
    public Deactivationhandler DeactivationDelegate;
    public delegate ActorBase ObjectPoolDelegate();
    public ObjectPoolDelegate objectPoolDelegate;
    #endregion

    public event Action OnUpdate;
    public event Action OnFixedUpdate;
    public event Action OnDisableEvent;
    public Action GameOverEvent;
    public Action GameWonEvent;

    public MainMenuScriptable mainMenuFunctions;

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject gameOverObject;
    [SerializeField] private GameObject gameWonObject;
    [SerializeField] private GameObject finishLineObject;

    [SerializeField] private TMP_Text highScoreText;
    private float highScore = int.MaxValue;

    public UIElementsData UiElementsData;

    [SerializeField] private TimerData timerData;

    public ObjectPool ObjectPool = new();

    public Bullets bullets;
    public EnemyData enemyData;

    public TimerScript timer;

    public WeaponData[] Weapons = new WeaponData[1];

    #region Dictionaries and Lists


    #endregion

    public IDamageableActor damageable;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        timer = new(this, canvas.transform, timerData);
        new Player(this, playerData, canvas.transform);
        damageable = new(enemyData);

        gameOverObject.SetActive(false);
        gameWonObject.SetActive(false);

        SetupGameStates();

        SaveToJson<int> saveToJson = new SaveToJson<int>();
        highScore = saveToJson.ReturnSavedInt();
        highScoreText.text = $"Highscore = {Mathf.RoundToInt(highScore)}";

        GameOverEvent += GameOver;
        GameWonEvent += GameWon;
        objectPoolDelegate += ObjectPool.GetPooledObjects;
        DeactivationDelegate += ObjectPool.DeActivate;
    }

    private void ReturnToMenu()
    {
        mainMenuFunctions.ReturnToMainMenu();
    }

    private void GameWon()
    {
        gameWonObject.SetActive(true);
        GameOverEvent = null;
        OnUpdate = null;
        OnFixedUpdate = null;
        OnDisableEvent = null;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (timer.ReturnTimeInSeconds() < highScore || highScore == 0)
        {
            SaveToJson<int> saveToJson = new SaveToJson<int>();
            saveToJson.SaveObjectToJson(Mathf.RoundToInt((float)timer.ReturnTimeInSeconds()));
        }

        Invoke(nameof(ReturnToMenu), 5.0f);
    }

    private void GameOver()
    {
        gameOverObject.SetActive(true);
        GameOverEvent = null;
        OnUpdate = null;
        OnFixedUpdate = null;
        OnDisableEvent = null;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Invoke(nameof(ReturnToMenu), 5.0f);
    }

    private void SetupGameStates()
    {
        fsm.AddState(new InstantiateGameObjects(fsm, ObjectPool, this));
        fsm.SwitchState(typeof(InstantiateGameObjects));
    }

    private void Update()
    {
        fsm.OnUpdate();
        timer.UpdateTimerElement();
        OnUpdate?.Invoke();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();

        if (Vector3.Distance(playerData.ActorMesh.transform.position, finishLineObject.transform.position) < 5.0f)
        {
            GameWonEvent?.Invoke();
        }
    }

    private void OnDisable()
    {
        OnDisableEvent?.Invoke();
        OnFixedUpdate = null;
        OnUpdate = null;
        OnDisableEvent = null;
    }
}
