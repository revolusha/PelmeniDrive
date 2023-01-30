using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]

public class ArcadeConfigurator : MonoBehaviour
{
    [Header("Puzzle")]
    [SerializeField] private float _timeToResetPuzzleSerializeField = 4;

    [Header("Puzzle Dots")]
    [SerializeField] private GameObject _dotTemplateSerializeField;
    [SerializeField] private Transform _dotParentSerializeField;
    [SerializeField] private PuzzleConfiguration _puzzleConfigurationSerializeField = 
        new PuzzleConfiguration { DotCount = 6, PuzzlesToSolve = 10};

    [Header("Spawn Points")]
    [SerializeField] private GameObject _dotSpawnpointTemplateSerializeField;
    [SerializeField] private GameObject _spawnPointsParentSerializeField;
    [SerializeField] private Vector3 _spawnPointsOriginSerializeField;
    [SerializeField] private PuzzleBoardCofiguration _boardCofigurationSerializeField = 
        new PuzzleBoardCofiguration { Columns = 4, Rows = 4, SideDistanceBetweenDots = 130, DownDistanceBetweenDots = 120};

    [Header("Other dependencies")]
    [SerializeField] private CookingStateAnimatorOrderController _animatorsController;

    [Header("Win Event")]
    [SerializeField] private UnityEvent _playerWon;

    private static float _timeToResetPuzzle = 4;
    private static Vector3 _spawnPointsOrigin;
    private static PuzzleBoardCofiguration _boardCofiguration;
    private static PuzzleConfiguration _puzzleConfiguration;
    private static GameObject _dotTemplate;
    private static Transform _dotParent;
    private static GameObject _dotSpawnpointTemplate;
    private static GameObject _spawnPointsParent;

    private static Timer _timer;

    private void OnEnable()
    {
        Prepare();
        _timer = GetComponent<Timer>();
    }

    private void Start()
    {
        Game.Arcade.OnPuzzleSolved += ChangeCookingState;
        Game.Arcade.OnAllPuzzlesSolved += TriggerWinEvent;
        _timer.OnRing += OnTimerRingEvent;
    }

    private void OnDisable()
    {
        Game.Arcade.OnPuzzleSolved -= ChangeCookingState;
        Game.Arcade.OnAllPuzzlesSolved -= TriggerWinEvent;
        _timer.OnRing -= OnTimerRingEvent;
    }

    public static Transform[] CreateDotSpawnPoints()
    {
        Transform[] result = new Transform[_boardCofiguration.Columns * _boardCofiguration.Rows];

        Vector3 offset = new Vector3(
            _spawnPointsParent.transform.localPosition.x + _spawnPointsOrigin.x,
            _spawnPointsParent.transform.localPosition.y + _spawnPointsOrigin.y);

        for (int i = 0; i < _boardCofiguration.Columns; i++)
        {
            for (int j = 0; j < _boardCofiguration.Rows; j++)
            {
                int index = i * _boardCofiguration.Columns + j;

                Vector3 newPosition = new Vector3(
                    offset.x + _boardCofiguration.SideDistanceBetweenDots * j, 
                    offset.y + _boardCofiguration.DownDistanceBetweenDots * i);

                GameObject newDot = Instantiate(_dotSpawnpointTemplate, newPosition, Quaternion.identity, _spawnPointsParent.transform);

                result[index] = newDot.transform;
            }
        }

        return result;
    }

    public void ChangeCookingState()
    {
        _animatorsController.TurnToState(Game.Arcade.SolvedPuzzlesCounter);
    }

    public static void ResetTimer()
    {
        _timer.SetTimer(_timeToResetPuzzle);
    }

    public static PuzzleConfiguration GetPuzzleConfiguration()
    {
        return _puzzleConfiguration;
    }

    public static DotRepresentation CreateDotRepresentation()
    {
        GameObject newDot = Instantiate(_dotTemplate, Vector3.zero, Quaternion.identity, _dotParent.transform);

        return newDot.GetComponent<DotRepresentation>();
    }

    private void OnTimerRingEvent()
    {
        Game.Arcade.RestartPuzzleGame();
    }

    private void TriggerWinEvent()
    {
        _playerWon?.Invoke();
    }

    private void Prepare()
    {
        _timeToResetPuzzle = _timeToResetPuzzleSerializeField;
        _spawnPointsOrigin = _spawnPointsOriginSerializeField;
        _boardCofiguration = _boardCofigurationSerializeField;
        _puzzleConfiguration = _puzzleConfigurationSerializeField;
        _dotParent = _dotParentSerializeField;
        _dotTemplate = _dotTemplateSerializeField;
        _dotSpawnpointTemplate = _dotSpawnpointTemplateSerializeField;
        _spawnPointsParent = _spawnPointsParentSerializeField;
    }
}