using System;
using System.Collections;
using UnityEngine;

public class PlayerTruckColliding : MonoBehaviour
{
    const float treePunchDelay = 0.6f;

    public Action OnTreePunch;
    public Action<Vector3> OnCarImpactWithPosition;
    public Action OnCarImpact;

    private Player _player;

    private void OnEnable()
    {
        _player = Game.Player;
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
            OnCarImpact?.Invoke();
        }

        if (other.GetComponent<Border>() != null)
        {
            _player.TriggerTreeDeath();
            KeepMovingUntilTreePunch();
        }

        if (other.GetComponent<BuildingHider>() != null)
            other.GetComponent<BuildingHider>().Hide();
    }

    private void KeepMovingUntilTreePunch()
    {
        StartCoroutine(StopPlayerAfterTreePunchEvent());
    }

    private IEnumerator StopPlayerAfterTreePunchEvent()
    {
        yield return new WaitForSeconds(treePunchDelay);
        _player.PlayDead();
        OnTreePunch?.Invoke();
    }
}
