using System;
using System.Collections;

public class Player
{
    private readonly Steering _steering;

    public Player(Steering steering)
    {
        IsCanControl = true;
        IsDead = false;
        _steering = steering;
    }

    public Action OnDead;
    public Action OnGettingDeadByTree;
    public Action OnTreePunch;

    public bool IsDead { get; private set; }
    public bool IsCanControl { get; private set; }

    public void Steer(float SteeringAccelerateFactor)
    {
        _steering.EffectAcceleration(SteeringAccelerateFactor);
    }

    public void TriggerTreeDeath()
    {
        IsCanControl = false;
        _steering.MakeTreePunchTurn();
        OnGettingDeadByTree?.Invoke();
    }

    public void PlayDead()
    {
        IsDead = true;
        OnDead?.Invoke();
    }

    public float GetRotationImpact()
    {
        return _steering.RotationImpact;
    }

    public float GetSteeringWheelState()
    {
        return _steering.SteeringWheelState;
    }

    public void GetPunchedByTree()
    {
        PlayDead();
        OnTreePunch?.Invoke();
    }
}
