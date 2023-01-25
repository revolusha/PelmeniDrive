using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class ViewChanger : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 6;
    [SerializeField] private float _seatFOV = 40;
    [SerializeField] private float _angleToTurnOffArcadeCamera = 60;
    [SerializeField] private Transform _seatViewPoint;
    [SerializeField] private GameObject _arcadeCanvas;

    public Action OnToggleToSeat;

    private bool _isLookingForward;
    private bool _canMove;

    private float _defaultFOV;
    private float _targetFOV;

    private Vector3 _viewToSeatDirection;

    private Quaternion _defaultRotation = Quaternion.Euler(Vector3.zero);
    private Quaternion _targetRotation;

    private Camera _camera;
    private Coroutine _rotationJob;
    private Player _player;

    private void OnEnable()
    {
        _player = Game.Player;
        _canMove = true;
        _isLookingForward = true;
        _viewToSeatDirection = _seatViewPoint.localPosition - transform.localPosition;
        transform.localRotation = _defaultRotation;
        _camera = GetComponent<Camera>();
        _defaultFOV = _camera.fieldOfView;
        _player.OnDead += TriggerDeathCam;
        _player.OnDeadByTree += TriggerDeathCam;
        _arcadeCanvas.SetActive(false);
    }

    private void Update()
    {
        if (_canMove && Input.GetKeyDown(KeyCode.Space))
            ChangeView();
    }

    private void OnDisable()
    {
        _player.OnDead -= TriggerDeathCam;
        _player.OnDeadByTree -= TriggerDeathCam;
    }

    public void ChangeView()
    {
        if (_isLookingForward)
        {
            _isLookingForward = false;
            _targetRotation = Quaternion.LookRotation(_viewToSeatDirection, Vector3.up);
            _targetFOV = _seatFOV; 
            OnToggleToSeat?.Invoke();
        }
        else
        {
            _isLookingForward = true;
            _targetRotation = _defaultRotation;
            _targetFOV = _defaultFOV;
        }

        RestartRotation();
    }

    public void TriggerDeathCam()
    {
        _canMove = false;
        _targetRotation = _defaultRotation;
        _targetFOV = _defaultFOV;
        RestartRotation();
    }

    private void RestartRotation()
    {
        if (_rotationJob != null)
            StopCoroutine(_rotationJob);

        _rotationJob = StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        while (transform.localRotation != _targetRotation)
        {
            transform.localRotation = Quaternion.Euler(Vector3.Lerp(transform.localRotation.eulerAngles, _targetRotation.eulerAngles, Time.deltaTime * _rotationSpeed));
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _targetFOV, Time.deltaTime * _rotationSpeed);

            if (transform.localEulerAngles.y < _angleToTurnOffArcadeCamera)
                _arcadeCanvas.SetActive(false);
            else
                _arcadeCanvas.SetActive(true);

            yield return new WaitForEndOfFrame();
        }
    }
}
