using UnityEngine;

public class CarSpawner : EntitySpawner
{
    [SerializeField] protected float _carSpeed = 8;

    protected override void FindSpawnPoints()
    {
        _spawnPoints = GetComponentsInChildren<EnemySpawnPoint>();
    }

    protected override void PrepareEntity(GameObject entity)
    {
        entity.GetComponent<EnemyTruck>().SetSpeed(_carSpeed);
    }
}
