using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    const string _labelText = "Puzzles to solve\n";

    private Arcade _arcade;
    private Text _text;

    private void OnEnable()
    {
        _text = GetComponentInChildren<Text>();
        _arcade = Game.Arcade;
        _arcade.OnPuzzleSolved += UpdateLabel;
    }

    private void Start()
    {
        UpdateLabel();        
    }

    private void OnDisable()
    {
        _arcade.OnPuzzleSolved -= UpdateLabel;
    }

    public void UpdateLabel()
    {
        _text.text = _labelText + _arcade.SolvedPuzzlesCounter + "/" + _arcade.PuzzlesToSolveTotal;
    }
}
