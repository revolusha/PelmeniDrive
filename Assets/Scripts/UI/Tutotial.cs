using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]

public class Tutotial : MonoBehaviour
{
    [SerializeField] private Text _description;
    [SerializeField] private Text _counter;
    [SerializeField] private TutorialHint[] _tutorials;

    private int _tutorialIndex = 0;

    private VideoPlayer _videoPlayer;

    private void OnEnable()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _tutorialIndex = _tutorials.Length;
        SkipToNext();
    }

    public void SkipToNext(bool isForward = true)
    {
        if (isForward)
            _tutorialIndex++;
        else
            _tutorialIndex--;

        if (_tutorialIndex >= _tutorials.Length)
            _tutorialIndex = 0;
        else if (_tutorialIndex < 0)
            _tutorialIndex += _tutorials.Length;

        _videoPlayer.clip = _tutorials[_tutorialIndex].Clip;
        _description.text = _tutorials[_tutorialIndex].Title + "\n\n" + _tutorials[_tutorialIndex].Descriptions;
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        _counter.text = (_tutorialIndex + 1).ToString() + "/"  + _tutorials.Length;
    }
}

[Serializable]
public struct TutorialHint
{
    public VideoClip Clip;
    public string Descriptions;
    public string Title;
}
