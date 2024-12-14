using System;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _minOffset = -10f;
    [SerializeField] private float _maxOffset = 10f;

    public event Action<Cube> CubeSpawned;

    public void Spawn()
    {
        Cube cube = Get();

        float randomX = UnityEngine.Random.Range(_minOffset, _maxOffset);
        float randomZ = UnityEngine.Random.Range(_minOffset, _maxOffset);

        cube.transform.position = new Vector3(randomX, gameObject.transform.position.y, randomZ);

        cube.ResetParameters();

        cube.Died += OnDied;
        CubeSpawned?.Invoke(cube);
    }

    private void OnDied(Cube cube)
    {
        cube.Died -= OnDied;
        Release(cube);
    }
}
