using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PlayerCrushSound : MonoBehaviour
{
    [SerializeField] private AudioClip _metal;
    [SerializeField] private AudioClip _tree;

    private AudioSource _output;
    private Player _player;

    private void OnEnable()
    {
        _player = Game.Player;
        _output = GetComponent<AudioSource>();
        _player.OnDead += PlayMetalSound;
        _player.OnTreePunch += PlayWoodSound;
    }

    private void OnDisable()
    {
        _player.OnDead -= PlayMetalSound;
        _player.OnTreePunch -= PlayWoodSound;
    }

    private void PlayWoodSound()
    {
        PlayCrushSound(_tree);
    }

    private void PlayMetalSound()
    {
        PlayCrushSound(_metal);
    }

    private void PlayCrushSound(AudioClip sound)
    {
        _output.PlayOneShot(sound);
        _player.OnDead -= PlayMetalSound;
        _player.OnTreePunch -= PlayWoodSound;
    }
}
