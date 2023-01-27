using System;
using System.Collections;
using UnityEngine;

public class PlayerTruckMoving : MonoBehaviour
{
    [SerializeField] private GameObject _steeringWheel;
    [SerializeField] private double _acceleration;
    [SerializeField] private double _truckSpeed;
    [SerializeField] private double _truckMaxSpeed;

    const float treePunchDelay = 0.6f;

    private Player _player;

    private float TruckSpeedFloat => ((float)_truckSpeed);

    private void OnEnable()
    {
        _player = Game.Player;
    }

    private void Update()
    {
        if (_player.IsDead)
            return;
        
        Move();
        ApplyAcceleration();
    }

    public void SetDifficulty(int difficulty)
    {
        const double EasyStartSpeed = 32;
        const double EasyMaxSpeed = 45;
        const double EasyAcceleration = 0.02;
        const double HardStartSpeed = 40;
        const double HardMaxSpeed = 80;
        const double HardAcceleration = 0.085;

        switch (difficulty)
        {
            case 1:
                _truckSpeed = HardStartSpeed;
                _truckMaxSpeed = HardMaxSpeed;
                _acceleration = HardAcceleration;
                break;

            default:
                _truckSpeed = EasyStartSpeed;
                _truckMaxSpeed = EasyMaxSpeed;
                _acceleration = EasyAcceleration;
                break;
        }
    }

    private void Move()
    {
        const float degree = 90;

        float _truckRotation = _player.GetRotationImpact();
        float _steeringWheelTurn = _player.GetSteeringWheelState();

        transform.SetPositionAndRotation(transform.position + TruckSpeedFloat * Time.deltaTime * transform.forward, Quaternion.Euler(0, _truckRotation * degree, 0));
        _steeringWheel.transform.localRotation = Quaternion.Euler(_steeringWheel.transform.localRotation.x, _steeringWheel.transform.localRotation.y, -_steeringWheelTurn * degree);
    }

    private void ApplyAcceleration()
    {
        if (_truckSpeed == _truckMaxSpeed)
            return;

        if (_truckSpeed > _truckMaxSpeed)
            _truckSpeed = _truckMaxSpeed;
        else
            _truckSpeed += _acceleration * Time.deltaTime;
    }

    public void KeepMovingUntilTreePunch()
    {
        StartCoroutine(StopPlayerAfterTreePunchEvent());
    }

    private IEnumerator StopPlayerAfterTreePunchEvent()
    {
        yield return new WaitForSeconds(treePunchDelay);
        _player.GetPunchedByTree();
    }
}
