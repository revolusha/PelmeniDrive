using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private Text _labelText;
    [SerializeField] private Text _scoreText;

    const string BoolName = "Show";
    const string LabelWin = "You are Winner!";
    const string LabelLose = "You are dead.";
    const string ScoreString = "Score: ";

    protected Player _player;
    protected Arcade _arcade;

    private Animator _animator;

    protected void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _player = Game.Player;
        _arcade = Game.Arcade;
        _arcade.OnAllPuzzlesSolved += ShowWin;
        _player.OnDead += ShowLose;
        _player.OnGettingDeadByTree += ShowLose;
    }

    private void OnDisable()
    {
        _arcade.OnAllPuzzlesSolved -= ShowWin;
        _player.OnDead -= ShowLose;
        _player.OnGettingDeadByTree -= ShowLose;
    }

    public void ShowMenu()
    {
        _scoreText.text = ScoreString + Game.Arcade.GetScore();
        _animator.SetBool(BoolName, true);
    }

    public void HideMenu()
    {
        _animator.SetBool(BoolName, false);
    }

    private void ShowLose()
    {
        _labelText.text = LabelLose;
        ShowMenu();
    }

    private void ShowWin()
    {
        _labelText.text = LabelWin;
        ShowMenu();
    }
}
