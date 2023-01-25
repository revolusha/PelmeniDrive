using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private PlayerTruckColliding _playerColliding;
    [SerializeField] private AudioClip _winMusic;
    [SerializeField] private AudioClip _loseMusic;
    [SerializeField] private AudioClip[] _musicArray;

    private int _currentTrack = -1;

    private Arcade _arcade;
    private AudioSource _music;
    private Coroutine _musicPlayJob;

    private void OnEnable()
    {
        _music = GetComponent<AudioSource>();
        _arcade = Game.Arcade;
        _playerColliding.OnCarImpact += PlayLoseMusic;
        _playerColliding.OnTreePunch += PlayLoseMusic;
        _arcade.OnAllPuzzlesSolved += PlayWinMusic;
    }

    private void Start()
    {
        SkipToNextTrack();        
    }

    private void OnDisable()
    {
        StopMusic();
        _playerColliding.OnCarImpact -= PlayLoseMusic;
        _playerColliding.OnTreePunch -= PlayLoseMusic;
    }

    public void PlayWinMusic()
    {
        StopMusic();
        _music.clip = _winMusic;
        _music.Play();
        _music.loop = true;
    }

    public void PlayLoseMusic()
    {
        StopMusic();
        _music.PlayOneShot(_loseMusic);
    }

    private void SkipToNextTrack()
    {
        int nextTrackIndex = Random.Range(0, _musicArray.Length);

        while (_currentTrack == nextTrackIndex)
        {
            nextTrackIndex = Random.Range(0, _musicArray.Length);
        }

        _currentTrack = nextTrackIndex;
        _music.PlayOneShot(_musicArray[_currentTrack]);
        RestartMusicTimer();
    }

    private void RestartMusicTimer()
    {
        if (_musicPlayJob != null)
            StopCoroutine(_musicPlayJob);

        _musicPlayJob = StartCoroutine(PlayTimedMusic());
    }

    private void StopMusic()
    {
        StopCoroutine(_musicPlayJob);
        _music.Stop();
    }

    private IEnumerator PlayTimedMusic()
    {
        float trackLength = _musicArray[_currentTrack].length;

        yield return new WaitForSeconds(trackLength);

        SkipToNextTrack();
    }
}
