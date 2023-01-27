using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject _playerTruck;
    [Tooltip("Steering wheel acceleration step")]
    [SerializeField] private float _steeringWheelAccelerationStep = 0.0001f;
    [Tooltip("Steering wheel acceleration limit")]
    [SerializeField] private float _steeringWheelAccelerationLimit = 0.005f;

    [Header("Others")]
    [SerializeField] private GameObject[] _objectsToReactivate;
    [SerializeField] private GameObject[] _objectsToShowOnPlay;

    private Player _player;
    private Arcade _arcade;

    public Action OnTick;

    public static Game Instance { get; private set; }

    public static Arcade Arcade => Instance._arcade;
    public static Player Player => Instance._player;
    public static GameObject PlayerTruck => Instance._playerTruck;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;        

        Initialize();
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
        ShowHiddenObjects(_objectsToReactivate);
    }

    private void Start()
    {
        _arcade.Initialize();
    }

    private void Update()
    {
        OnTick?.Invoke();
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        ShowHiddenObjects(_objectsToShowOnPlay);
    }

    private void Initialize()
    {
        if (_player != null || _arcade != null)
            return;

        _player = new Player(new Steering(_steeringWheelAccelerationStep, _steeringWheelAccelerationLimit));
        _arcade = new Arcade();
    }

    private void ShowHiddenObjects(GameObject[] _objectsToShow)
    {
        foreach (GameObject gameObject in _objectsToShow)
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);

            gameObject.SetActive(true);
        }
    }
}
