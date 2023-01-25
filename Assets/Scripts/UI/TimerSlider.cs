using UnityEngine;
using UnityEngine.UI;

public class TimerSlider
{
    private Slider _slider;
    private GameObject _gameObject;

    public TimerSlider(Slider slider)
    {
        _slider = slider;
        _gameObject = _slider.gameObject;
    }

    public void UpdateSlider(float value)
    {
        _slider.value = value;
    }

    public void SetMaxValue(float maxValue)
    {
        _slider.maxValue = maxValue;
    }

    public void Show()
    {
        _gameObject.SetActive(true);
    }

    public void Hide()
    {
        _gameObject.SetActive(false);
    }
}
