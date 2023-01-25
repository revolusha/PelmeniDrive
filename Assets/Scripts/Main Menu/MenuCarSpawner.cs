public class MenuCarSpawner : CarSpawner
{
    protected override void OnEnable()
    {
        FindSpawnPoints();
        TryPlaceEntity();
        RestartSpawning();
    }

    protected override void OnDisable()
    {
        StopSpawning();
    }

    private void RestartSpawning()
    {
        if (_spawning != null)
            StopCoroutine(_spawning);

        _spawning = StartCoroutine(StartEntitySpawning());
    }
}
