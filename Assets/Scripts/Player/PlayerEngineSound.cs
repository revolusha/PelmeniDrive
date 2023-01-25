using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PlayerEngineSound : MonoBehaviour
{
    [SerializeField] private AudioClip _engineNormal;
    [SerializeField] private AudioClip _engineBroken;

    private AudioSource _output;
    private PlayerTruckColliding _playerCollider;

    private void OnEnable()
    {
        _playerCollider = GetComponentInParent<PlayerTruckColliding>();
        _playerCollider.OnCarImpact += SetBrokenSound;
        _playerCollider.OnTreePunch += SetBrokenSound;
        _output = GetComponent<AudioSource>();
        ChangeSound(_engineNormal);
    }

    private void OnDisable()
    {
        _playerCollider.OnCarImpact -= SetBrokenSound;
        _playerCollider.OnTreePunch -= SetBrokenSound;
    }

    private void SetBrokenSound()
    {
        ChangeSound(_engineBroken);
    }

    private void ChangeSound(AudioClip sound)
    {
        _output.Stop();
        _output.clip = sound;
        _output.Play();
        _output.loop = true;
    }
}
