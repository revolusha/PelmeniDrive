using System;
using UnityEngine;

public class Dot
{
    private readonly DotRepresentation _representation;

    public Dot(DotRepresentation dotRepresentation)
    {
        _representation = dotRepresentation;
        _representation.OnClicked += Click;
        _representation.gameObject.SetActive(false);
    }

    public Action OnClicked;

    public bool IsClicked { get; set; }
    public DotRepresentation Representation => _representation;

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
}
