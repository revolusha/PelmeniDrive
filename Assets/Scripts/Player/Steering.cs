using UnityEngine;

public class Steering
{
    const float SteeringLimit = 0.6f;
    const float AlignmentTargetMinimalRange = 0.4f;
    const float AlignmentAccelerationMultiplier = 3;
    const float AlignmentTargetMinimalOffset = 0.05f;

    private readonly float _accelerationLimit;
    private readonly float _accelerationStep;

    private float _wheelsCurrentRotationState;
    private float _currentSteerAcceleration;
    private float _playerTruckTargetRotationState;

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
        if (Game.PlayerTruck.transform.localPosition.x < 0)
            _playerTruckTargetRotationState = -AlignmentTargetMinimalRange;
        else
            _playerTruckTargetRotationState = AlignmentTargetMinimalRange;

        _isOffTheRoad = true;
    }

    private void AlignSteering()
    {
        float AlignmentCorrectionTarget;

        if (Game.PlayerTruck.transform.localRotation.y > _playerTruckTargetRotationState + AlignmentTargetMinimalOffset)
            AlignmentCorrectionTarget = -AlignmentTargetMinimalRange;
        else if (Game.PlayerTruck.transform.localRotation.y < _playerTruckTargetRotationState - AlignmentTargetMinimalOffset)
            AlignmentCorrectionTarget = AlignmentTargetMinimalRange;
        else
            AlignmentCorrectionTarget = 0;

        _currentSteerAcceleration = Mathf.Lerp(
            _currentSteerAcceleration, 
            AlignmentCorrectionTarget, 
            _accelerationStep * Time.deltaTime * AlignmentAccelerationMultiplier * _accelerationStep);
    }
}
