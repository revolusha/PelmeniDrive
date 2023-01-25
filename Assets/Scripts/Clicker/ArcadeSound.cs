using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class ArcadeSound : MonoBehaviour
{
    [SerializeField] private AudioClip _correctClick;
    [SerializeField] private AudioClip _incorrectClick;

    private AudioSource _output;
    private Arcade _arcade;

    private void OnEnable()
    {
        _output = GetComponent<AudioSource>();
        _arcade = Game.Arcade;
        _arcade.OnPuzzleSolved += PlayCorrectSound;
        _arcade.OnWrongClicked += PlayIncorrectSound;
    }

    private void OnDisable()
    {
        _arcade.OnPuzzleSolved -= PlayCorrectSound;
        _arcade.OnWrongClicked -= PlayIncorrectSound;
    }

    private void PlayCorrectSound()
    {
        _output.PlayOneShot(_correctClick);
    }

    private void PlayIncorrectSound()
    {
        _output.PlayOneShot(_incorrectClick);
    }
}
