using System.Collections.Generic;
using UnityEngine;

public class DotHandler
{
    public readonly List<Dot> _dots = new List<Dot>();

    public PuzzleConfiguration _config;
    public Transform[] _spawnPoints;

    private readonly Arcade _arcade;

    public DotHandler(Arcade arcade)
    {
        _arcade = arcade;
    }

    public int SolvedPuzzlesCounter { get; private set; }
    public int MistakeCount { get; private set; }
    public int DotsClicked
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

    public void SetConfiguration(PuzzleConfiguration config)
    {
        _config = config;
    }

    public void CreateDotSpawnPoints()
    {
        _spawnPoints = ArcadeConfigurator.CreateDotSpawnPoints();
    }

    public void CreateDots()
    {
        for (int i = _dots.Count; i < _config.DotCount; i++)
        {
            Dot dot = new Dot(ArcadeConfigurator.CreateDotRepresentation());

            dot.OnClicked += _arcade.OnClickedAnyDotEvent;
            _dots.Add(dot);
        }
    }

    public void RefreshDots()
    {
        foreach (Dot dot in _dots)
            dot.Refresh();
    }

    public void HideDots()
    {
        foreach (var dot in _dots)
            dot.Representation.gameObject.SetActive(false);
    }

    public void ShowDots()
    {
        foreach (var dot in _dots)
            dot.Representation.gameObject.SetActive(true);
    }

    public void ReplaceDots()
    {
        int indexOfList;
        List<int> positions = GetIndexList();

        for (int i = 0; i < _config.DotCount; i++)
        {
            indexOfList = Random.Range(0, positions.Count);
            _dots[i].Representation.MoveToTransform(_spawnPoints[positions[indexOfList]]);
            _dots[i].Representation.SetText(i.ToString());
            positions.RemoveAt(indexOfList);
        }
    }

    public bool CheckClickOrder()
    {
        return _dots[DotsClicked - 1].IsClicked;
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
}