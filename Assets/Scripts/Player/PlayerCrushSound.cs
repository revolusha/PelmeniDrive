using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PlayerCrushSound : MonoBehaviour
{
    [SerializeField] private AudioClip _metal;
    [SerializeField] private AudioClip _tree;

    private AudioSource _output;
    private PlayerTruckColliding _playerCollider;

    private void OnEnable()
    {
        _output = GetComponent<AudioSource>();
        _playerCollider = GetComponentInParent<PlayerTruckColliding>();
        _playerCollider.OnCarImpact += PlayMetalSound;
        _playerCollider.OnTreePunch += PlayWoodSound;
    }

    private void OnDisable()
    {
        _playerCollider.OnCarImpact -= PlayMetalSound;
        _playerCollider.OnTreePunch -= PlayWoodSound;
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
        _playerCollider.OnCarImpact -= PlayMetalSound;
        _playerCollider.OnTreePunch -= PlayWoodSound;
    }
}
