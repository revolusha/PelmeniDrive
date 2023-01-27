using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]

public class WoodChipsEffector : MonoBehaviour
{
    private Player _player;
    private ParticleSystem _particleSystem;

    private void OnEnable()
    {
        _player = Game.Player;
        _player.OnTreePunch += Play;
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystem.Stop();
    }

    private void OnDisable()
    {
        _player.OnTreePunch -= Play;
    }

    private void Play()
    {
        _particleSystem.Play();
    }
}
