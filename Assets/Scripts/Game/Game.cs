using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    [Header("Player")]
    [Tooltip("Steering wheel acceleration step")]
    [SerializeField] private float _steeringWheelAccelerationStep = 0.0001f;
    [Tooltip("Steering wheel acceleration limit")]
    [SerializeField] private float _steeringWheelAccelerationLimit = 0.005f;

    [Header("Others")]
    [SerializeField] private GameObject[] _objectsToReactivate;

    private Player _player;
    private Arcade _arcade;

    public Action OnTick;
    public Action OnRestart;

    public static Game Instance { get; private set; }

    public static Arcade Arcade => Instance._arcade;
    public static Player Player => Instance._player;

    private void OnEnable()
    {
        OnRestart?.Invoke();

        if (Instance == null)
            Instance = this;

        Initialize();
        Time.timeScale = 0;
        ReactivateHiddenObjects();
    }

    private void Update()
    {
        OnTick?.Invoke();
    }

    public void Unpause()
    {
        Time.timeScale = 1;
    }

    private void Initialize()
    {
        if (_player != null || _arcade != null)
            return;

        _player = new Player(new Steering(_steeringWheelAccelerationStep, _steeringWheelAccelerationLimit));
        _arcade = new Arcade();
    }

    private void ReactivateHiddenObjects()
    {
        foreach (GameObject gameObject in _objectsToReactivate)
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);

            gameObject.SetActive(true);
        }
    }
}
