using System;
using UnityEngine;

public class ArcadePanels : MonoBehaviour
{
    public Action OnSetActive;

    private void OnEnable()
    {
        OnSetActive?.Invoke();
    }
}
