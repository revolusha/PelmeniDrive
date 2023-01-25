using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Player _player;

    private void OnEnable()
    {
        _player = Game.Player;
    }

    private void Update()
    {
        if (_player.IsDead || _player.IsCanControl == false)
            return;
        
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            _player.Steer(-Time.deltaTime);

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            _player.Steer(Time.deltaTime);
    }
}
