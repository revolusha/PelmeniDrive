using System;
using UnityEngine;

public class Arcade
{
    private readonly DotHandler _dotHandler;

    private int _mistakeCount;
    private PuzzleConfiguration _config;

    public Arcade()
    {
        _dotHandler = new DotHandler(this);
    }

    public Action OnWrongClicked;
    public Action OnPuzzleSolved;
    public Action OnAllPuzzlesSolved;
    
    public int SolvedPuzzlesCounter { get; private set; }
    public int PuzzlesToSolveTotal { get; private set; }
    public bool IsWinReached => SolvedPuzzlesCounter == PuzzlesToSolveTotal;

    public void Initialize()
    {
        PreparePuzzleGame();
        ResetPuzzle();
    }

    public void RestartPuzzleGame()
    {
        _dotHandler.RefreshDots();
        GenerateClickPuzzle();
    }

    public double GetScore()
    {
        const float MistakePenalty = 5;
        const float ScoreMultiplier = 100;

        float score = Time.timeSinceLevelLoad * ScoreMultiplier - MistakePenalty * _mistakeCount;

        return Mathf.Round(score);
    }

    public void GenerateClickPuzzle()
    {
        _dotHandler.RefreshDots();
        _dotHandler.ReplaceDots();
        _dotHandler.ShowDots();
    }

    public void ResetPuzzle()
    {
        _dotHandler.HideDots();
        ArcadeConfigurator.ResetTimer();
    }

    public void OnClickedAnyDotEvent()
    {
        if (_dotHandler.CheckClickOrder() == false)
        {
            _mistakeCount++;
            OnWrongClicked?.Invoke();
            ResetPuzzle();
            return;
        }

        if (_dotHandler.DotsClicked == _config.DotCount)
            ScoreUp();
    }

    private void ScoreUp()
    {
        SolvedPuzzlesCounter++;
        OnPuzzleSolved?.Invoke();

        if (IsWinReached)
            CheckForWin();

        ResetPuzzle();
    }

    private void CheckForWin()
    {
        _dotHandler.HideDots();
        OnAllPuzzlesSolved?.Invoke();
        return;
    }

    private void PreparePuzzleGame()
    {
        _config = ArcadeConfigurator.GetPuzzleConfiguration();
        PuzzlesToSolveTotal = _config.PuzzlesToSolve;
        _dotHandler.SetConfiguration(_config);
        _dotHandler.CreateDotSpawnPoints();
        _dotHandler.CreateDots();
        _mistakeCount = 0;
        GenerateClickPuzzle();
    }
}

[Serializable]
public struct PuzzleConfiguration
{
    public int PuzzlesToSolve;
    public int DotCount;
}

[Serializable]
public struct PuzzleBoardCofiguration
{
    public int Columns;
    public int Rows;
    public float SideDistanceBetweenDots;
    public float DownDistanceBetweenDots;
}
