using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioPitchRandomizer : MonoBehaviour
{
    [Range(-3f, 3f)]
    [SerializeField] private float _minimumPitch = 0.7f;
    [Range(-3f, 3f)]
    [SerializeField] private float _maximumPitch = 1.3f;

    private void OnEnable()
    {

        if (_minimumPitch > _maximumPitch)
        {
            float buffer = _maximumPitch;
            _maximumPitch = _minimumPitch;
            _minimumPitch = buffer;
        }

        GetComponent<AudioSource>().pitch = Random.Range(_minimumPitch, _maximumPitch);
    }
}
