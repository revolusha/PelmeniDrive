using UnityEngine;

public class Steering
{
    const float SteeringLimit = 0.6f;

    private readonly float _accelerationLimit;
    private readonly float _accelerationStep;

    private float _currentSteerAcceleration;
    private float _wheelsCurrentRotationState;

    private bool _isOffTheRoad;

    public Steering(float accelerationStep, float accelerationLimit)
    {
        _accelerationStep = accelerationStep;
        _accelerationLimit = accelerationLimit;
        _currentSteerAcceleration = 0;
        _wheelsCurrentRotationState = 0;
        _isOffTheRoad = false;
        Game.Instance.OnTick += Tick;
    }

    public float RotationImpact => _wheelsCurrentRotationState;
    public float SteeringWheelState => _currentSteerAcceleration;

    public void Tick()
    {        
        if (_isOffTheRoad)
            AlignSteering();

        _wheelsCurrentRotationState += _currentSteerAcceleration * Time.deltaTime;
        _wheelsCurrentRotationState = Mathf.Clamp(_wheelsCurrentRotationState, -SteeringLimit, SteeringLimit);
    }

    public void EffectAcceleration(float Value)
    {
        if (_isOffTheRoad)
            return;

        if (Value < 0)
            _currentSteerAcceleration += _accelerationStep * Value;
        else if (Value > 0)
            _currentSteerAcceleration += _accelerationStep * Value;
        else
            _currentSteerAcceleration = 0;

        _currentSteerAcceleration = Mathf.Clamp(_currentSteerAcceleration, -_accelerationLimit, _accelerationLimit);
    }

    public void MakeTreePunchTurn()
    {
        _isOffTheRoad = true;
    }

    private void AlignSteering()
    {
        const float AlignmentAcceleration = 6;

        _currentSteerAcceleration = Mathf.Lerp(_currentSteerAcceleration, 0, _accelerationStep * Time.deltaTime * AlignmentAcceleration);
    }
}
