using UnityEngine;

public class PlayerTruckMoving : MonoBehaviour
{
    [SerializeField] private GameObject _steeringWheel;
    [SerializeField] private double _truckSpeed = 40;
    [SerializeField] private double _truckMaxSpeed = 40;
    [SerializeField] private double _acceleration = 0.001;

    private Player _player;

    private float _truckSpeedFloat => ((float)_truckSpeed);

    private void OnEnable()
    {
        _player = Game.Player;
    }

    private void Update()
    {
        if (_player.IsDead)
            return;
        
        Move();
    }

    private void Move()
    {
        const float degree = 90;

        float _truckRotation = _player.GetRotationImpact();
        float _steeringWheelTurn = _player.GetSteeringWheelState();

        transform.SetPositionAndRotation(transform.position + _truckSpeedFloat * Time.deltaTime * transform.forward, Quaternion.Euler(0, _truckRotation * degree, 0));
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
}
