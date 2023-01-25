using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]

public class WoodChipsEffector : MonoBehaviour
{
    private PlayerTruckColliding _playerTruckColliding;
    private ParticleSystem _particleSystem;

    private void OnEnable()
    {
        _playerTruckColliding = GetComponentInParent<PlayerTruckColliding>();
        _playerTruckColliding.OnTreePunch += Play;
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystem.Stop();
    }

    private void OnDisable()
    {
        _playerTruckColliding.OnTreePunch -= Play;
    }

    private void Play()
    {
        _particleSystem.Play();
    }
}
