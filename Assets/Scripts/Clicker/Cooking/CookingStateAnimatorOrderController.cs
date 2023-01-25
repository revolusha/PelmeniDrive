using System;
using System.Collections.Generic;
using UnityEngine;

public class CookingStateAnimatorOrderController : MonoBehaviour
{
    [SerializeField] private List<CookingStateAnimator> _states;

    public void TurnToState(int state)
    {
        if (state == 0 || _states.Count <= state || _states[state] == null)
            throw new InvalidOperationException( state + " - Invalid Cooking State!");

        for (int i = 0; ++i < state + 1;)
        {
            _states[i].Play(state);
        }
    }
}
