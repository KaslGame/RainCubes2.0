using System;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    public event Action<Cube> CubeSpawned;

    public void Spawn()
    {
        Cube cube = Get();

        float randomX = UnityEngine.Random.Range(-10, 10);
        float randomZ = UnityEngine.Random.Range(-10, 10);

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
