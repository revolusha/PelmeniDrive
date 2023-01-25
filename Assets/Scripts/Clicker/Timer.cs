using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private bool IsStoped;

    private float _timePassed;
    private float _target;

    public Action OnRing;

    private TimerSlider _timerSlider;

    private void OnEnable()
    {
        IsStoped = true;
        _timerSlider = new TimerSlider(_slider);
    }

    private void Update()
    {
        Tick();
        _timerSlider.UpdateSlider(_timePassed);
    }

    public void SetTimer(float seconds)
    {
        IsStoped = false;
        _timePassed = 0;
        _target = seconds;
        _timerSlider.SetMaxValue(seconds);
        _timerSlider.Show();
    }

    private void Tick()
    {
        if (IsStoped)
            return;

        _timePassed += Time.deltaTime;

        if (_timePassed > _target)
            Ring();
    }    

    private void Ring()
    {
        IsStoped = true;
        _timerSlider.Hide();
        OnRing?.Invoke();
    }
}
