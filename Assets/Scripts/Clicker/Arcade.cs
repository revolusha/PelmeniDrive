using System;
using System.Collections.Generic;
using UnityEngine;

public class Arcade
{
    private int _mistakeCount;

    private PuzzleConfiguration _config;

    private readonly List<Dot> _dots = new List<Dot>();

    private Transform[] _spawnPoints;

    public Action OnWrongClicked;
    public Action OnPuzzleSolved;
    public Action OnAllPuzzlesSolved;

    public int SolvedPuzzlesCounter { get; private set; }
    public int PuzzlesToSolveTotal { get; private set; }

    private int DotsClicked
    {
        get
        {
            int sum = 0;

            for (int i = 0; i < _dots.Count; i++)
                if (_dots[i].IsClicked)
                    sum++;

            return sum;
        }
    }

    public void Initialize()
    {
        PreparePuzzleGame();
        ResetPuzzle();
    }

    public void RestartPuzzleGame()
    {
        RefreshDots();
        GenerateClickPuzzle();
    }

    public void PreparePuzzleGame()
    {
        _mistakeCount = 0;
        GetPuzzleConfiguration();
        PuzzlesToSolveTotal = _config.PuzzlesToSolve;
        CreateDotSpawnPoints();
        CreateDots();
        GenerateClickPuzzle();
    }

    public double GetScore()
    {
        const float MistakePenalty = 5;
        const float ScoreMultiplier = 100;

        float score = Time.timeSinceLevelLoad * ScoreMultiplier - MistakePenalty * _mistakeCount;

        return Mathf.Round(score);
    }

    private void GenerateClickPuzzle()
    {
        ReplaceDots();
        ShowDots();
    }

    private void ReplaceDots()
    {
        int indexOfList;
        List<int> positions = GetIndexList();

        for (int i = 0; i < _config.DotCount; i++)
        {
            indexOfList = UnityEngine.Random.Range(0, positions.Count);
            _dots[i].MoveToTransform(_spawnPoints[positions[indexOfList]]);
            _dots[i].SetText(i.ToString());
            positions.RemoveAt(indexOfList);
        }
    }

    private List<int> GetIndexList()
    {
        List<int> list = new List<int>();

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            int index = i;

            list.Add(index);
        }

        return list;
    }

    private void CheckClickOrder()
    {
        int dotsClicked = DotsClicked;

        if (_dots[dotsClicked - 1].IsClicked == false)
        {
            _mistakeCount++;
            OnWrongClicked?.Invoke();
            ResetPuzzle();
            return;
        }

        if (dotsClicked == _config.DotCount)
        {
            SolvedPuzzlesCounter++;
            OnPuzzleSolved?.Invoke();

            if (_config.PuzzlesToSolve == SolvedPuzzlesCounter)
            {
                HideDots();
                OnAllPuzzlesSolved?.Invoke();
                return;
            }

            ResetPuzzle();
        }
    }

    private void ResetPuzzle()
    {
        HideDots();
        ArcadeController.ResetTimer();
    }

    private void CreateDots()
    {
        for (int i = _dots.Count; i < _config.DotCount; i++)
        {
            Dot dot = new Dot(ArcadeController.CreateDotRepresentation());

            dot.OnClicked += CheckClickOrder;
            _dots.Add(dot);
        }
    }

    private void RefreshDots()
    {
        foreach (Dot dot in _dots)
            dot.Refresh();
    }

    private void HideDots()
    {
        foreach (var dot in _dots)
        {
            dot.Hide();
        }
    }

    private void ShowDots()
    {
        foreach (var dot in _dots)
            dot.Show();
    }

    private void CreateDotSpawnPoints()
    {
        _spawnPoints = ArcadeController.CreateDotSpawnPoints();
    }

    private void GetPuzzleConfiguration()
    {
        _config = ArcadeController.GetPuzzleConfiguration();
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
