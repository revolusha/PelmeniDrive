using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerTruckMoving))]

public class PlayerTruckColliding : MonoBehaviour
{
    public Action<Vector3> OnCarImpactWithPosition;

    private Player _player;
    private PlayerTruckMoving _moving;

    private void OnEnable()
    {
        _player = Game.Player;
        _moving = GetComponent<PlayerTruckMoving>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_player.IsDead)
            return;

        if (other.GetComponent<EnemyTruck>() != null)
        {
            _player.PlayDead();
            other.GetComponent<EnemyTruck>().SetSpeed(0);
            OnCarImpactWithPosition?.Invoke(other.ClosestPoint(transform.position));
        }

        if (other.GetComponent<Border>() != null)
        {
            _player.TriggerTreeDeath();
            _moving.KeepMovingUntilTreePunch();
        }

        if (other.GetComponent<BuildingHider>() != null)
            other.GetComponent<BuildingHider>().Hide();
    }
}
