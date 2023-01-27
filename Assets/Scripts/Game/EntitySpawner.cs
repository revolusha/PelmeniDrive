using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntitySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _entityTemplate;

    [SerializeField] protected Transform _parentTransform;
    [SerializeField] protected int _poolSize = 10;
    [SerializeField] protected float _spawnTimeInterval = 5;

    protected SpawnPoint[] _spawnPoints;
    protected Coroutine _spawning;

    private List<GameObject> _entityPool = new List<GameObject>();
    private Player _player;

    protected virtual void OnEnable()
    {
        _player = Game.Player;
        FindSpawnPoints();
        TryPlaceEntity();
        _spawning = StartCoroutine(StartEntitySpawning());
        _player.OnDead += StopSpawning;
        _player.OnGettingDeadByTree += StopSpawning;
    }
    protected virtual void OnDisable()
    {
        _player.OnDead -= StopSpawning;
        _player.OnGettingDeadByTree -= StopSpawning;
        StopSpawning();
    }

    protected abstract void PrepareEntity(GameObject entity);

    protected abstract void FindSpawnPoints();

    private GameObject GetEntityTemplate()
    {
        return _entityTemplate[Random.Range(0, _entityTemplate.Length)];
    }

    private GameObject GetInactiveEntity()
    {
        GameObject inactiveTree = null;

        for (int i = 0; i < _entityPool.Count; i++)
        {
            if (_entityPool[i].activeSelf == false)
            {
                inactiveTree = _entityPool[i];
                break;
            }
        }

        return inactiveTree;
    }

    private GameObject TryGetReadyEntity()
    {
        GameObject idlingEntity = GetInactiveEntity();

        if (idlingEntity == null)
        {
            if (_entityPool.Count < _poolSize)
            {
                idlingEntity = Instantiate(GetEntityTemplate(), _spawnPoints[0].transform.position, Quaternion.identity, _parentTransform);
                _entityPool.Add(idlingEntity);
            }
            else
                return null;
        }
        
        return idlingEntity;
    }

    protected void TryPlaceEntity()
    {
        int spawnPointIndex = Random.Range(0, _spawnPoints.Length);
        GameObject idlingEntity = TryGetReadyEntity();

        if (idlingEntity == null)
            return;

        idlingEntity.transform.position = _spawnPoints[spawnPointIndex].transform.position;
        PrepareEntity(idlingEntity);
        idlingEntity.SetActive(true);
    }

    protected void StopSpawning()
    {
        if (_spawning != null)
            StopCoroutine(_spawning);
    }

    protected IEnumerator StartEntitySpawning()
    {
        while (_entityPool.Count > 0)
        {
            yield return new WaitForSeconds(_spawnTimeInterval);
            TryPlaceEntity();
        }
    }
}
