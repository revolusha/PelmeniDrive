using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class DotRepresentation : MonoBehaviour
{
    [SerializeField] private Color _pressedColor = Color.blue;
    [SerializeField] private Text _text;

    public Action OnClicked;

    private AudioSource _output;

    private void OnEnable()
    {
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
        GetComponent<Image>().color = _pressedColor;
    }

    public void ResetDot()
    {
        GetComponent<Image>().color = Color.white;
    }

    public void MoveToTransform(Transform transform)
    {
        this.transform.localPosition = transform.position;
    }
}
