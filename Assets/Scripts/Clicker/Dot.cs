using System;
using UnityEngine;

public class Dot
{
    private DotRepresentation _representation;
    private GameObject _gameObject;

    public Dot(DotRepresentation dotRepresentation)
    {
        _representation = dotRepresentation;
        _gameObject = dotRepresentation.gameObject;
        _representation.OnClicked += Click;
        Hide();
    }

    public Action OnClicked;

    public bool IsClicked { get; set; }

    public void Hide()
    {
        _gameObject.SetActive(false);
    }

    public void Show()
    {
        _gameObject.SetActive(true);
    }

    public void Click()
    {
        if (IsClicked)
        {
            return;
        }
        else
        {
            IsClicked = true;
            _representation.SetSquareColorToPressed();
            OnClicked?.Invoke();
        }
    }

    public void Refresh()
    {
        _representation.ResetDot();
        IsClicked = false;
    }

    public void SetText(string text)
    {
        _representation.SetText(text);
    }

    public void MoveToTransform(Transform transform)
    {
        _representation.transform.localPosition = transform.position;
    }
}
