using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]

public class SparklesEffector : MonoBehaviour
{
    [SerializeField] private Vector3 effectPositionOffset = new Vector3(0, 1, 0);

    private PlayerTruckColliding _playerTruckColliding;
    private ParticleSystem _particleSystem;

    private void OnEnable()
    {
        _playerTruckColliding = GetComponentInParent<PlayerTruckColliding>();
        _playerTruckColliding.OnCarImpactWithPosition += Play;
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystem.Stop();
    }

    private void OnDisable()
    {
        _playerTruckColliding.OnCarImpactWithPosition -= Play;
    }

    private void Play(Vector3 effectNewWorldPosition)
    {
        transform.position = effectNewWorldPosition + effectPositionOffset;
        _particleSystem.Play();
    }
}
