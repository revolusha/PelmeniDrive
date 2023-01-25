using UnityEngine;

public class TreeSpawner : EntitySpawner
{
    protected override void FindSpawnPoints()
    {
        _spawnPoints = GetComponentsInChildren<TreeSpawnPoint>();
    }

    protected override void PrepareEntity(GameObject entity)
    {
        const float MaxYRotation = 180;
        const float MinYRotation = -180;

        entity.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(MinYRotation, MaxYRotation), 0));
    }
}
