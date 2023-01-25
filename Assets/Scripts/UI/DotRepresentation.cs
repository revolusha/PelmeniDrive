using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class DotRepresentation : MonoBehaviour
{
    [SerializeField] private Color _pressedColor = Color.blue;
    [SerializeField] private Text _text;

    public Action OnClicked;

    private Image _image;
    private AudioSource _output;


    private void OnEnable()
    {
        if (_image == null)
            _image = GetComponent<Image>();

        _output = GetComponentInParent<AudioSource>();
    }

    public void SetTextColor(Color color)
    {
        _text.color = color;
    }

    public void SetText(string text)
    {
        _text.text = text;
    }

    public void Click()
    {
        _output.Play();
        OnClicked?.Invoke();
    }

    public void SetSquareColorToPressed()
    {
        _image.color = _pressedColor;
    }

    public void ResetDot()
    {
        _image.color = Color.white;
    }
}
