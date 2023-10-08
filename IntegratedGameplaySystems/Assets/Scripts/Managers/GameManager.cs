using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ActorData playerData;

    public delegate void Deactivationhandler(ActorBase bullet);
    public Deactivationhandler DeactivationDelegate;
    public delegate ActorBase ObjectPoolDelegate();
    public ObjectPoolDelegate objectPoolDelegate;

    public ObjectPool ObjectPool;
    public Bullets bullets;
    public EnemyData enemyData;

    public UIElementsData UiElementsData;

    public TimerScript timer;

    public WeaponData[] Weapons = new WeaponData[1];

    public IDamageableActor damageable;

    public Action OnUpdate;
    public Action OnFixedUpdate;
    public Action OnDisableEvent;
    public Action GameOverEvent;
    public Action GameWonEvent;

    public MainMenuScriptable mainMenuFunctions;

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject gameOverObject;
    [SerializeField] private GameObject gameWonObject;
    [SerializeField] private GameObject finishLineObject;

    [SerializeField] private TimerData timerData;
    [SerializeField] private TMP_Text highScoreText;

    private float highScore = int.MaxValue;

    private readonly FSM<GameManager> fsm = new();

    private bool gameWon = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameWon = false;

        ObjectPool = new(this);
        timer = new(this, canvas.transform, timerData);
        new Player(this, playerData, canvas.transform);
        damageable = new(enemyData);

        gameOverObject.SetActive(false);
        gameWonObject.SetActive(false);

        SetupGameStates();

        //"Highscore" Is the file name it will make/read. So you can use it to store other things if needed.
        SaveToJson<int> saveToJson = new("Highscore");
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
        OnDisableEvent?.Invoke();
        gameWonObject.SetActive(true);
        OnUpdate = null;
        OnFixedUpdate = null;
        GameOverEvent = null;
        GameWonEvent = null;
        objectPoolDelegate = null;
        DeactivationDelegate = null;
        OnDisableEvent = null;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (timer.ReturnTimeInSeconds() < highScore || highScore == 0)
        {
            SaveToJson<int> saveToJson = new("Highscore");
            saveToJson.SaveObjectToJson(Mathf.RoundToInt((float)timer.ReturnTimeInSeconds()));
        }

        Invoke(nameof(ReturnToMenu), 5.0f);
    }

    private void GameOver()
    {
        OnDisableEvent?.Invoke();
        gameOverObject.SetActive(true);
        OnUpdate = null;
        OnFixedUpdate = null;
        GameOverEvent = null;
        GameWonEvent = null;
        objectPoolDelegate = null;
        DeactivationDelegate = null;
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

        if (Vector3.Distance(playerData.ActorMesh.transform.position, finishLineObject.transform.position) < 5.0f && !gameWon)
        {
            gameWon = true;
            GameWonEvent?.Invoke();
        }
    }

    private void OnDisable()
    {
        OnDisableEvent?.Invoke();

        DeactivationDelegate = null;
        objectPoolDelegate = null;
        OnUpdate = null;
        OnFixedUpdate = null;
        GameOverEvent = null;
        GameWonEvent = null;
        OnDisableEvent = null;
    }
}
