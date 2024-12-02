using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerContoller : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private float _repeatingRate = 1f;

    private void Start()
    {
        StartCoroutine(FillPool(_repeatingRate));
    }

    private void OnEnable()
    {
        _cubeSpawner.CubeSpawned += OnCubeSpawned;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeSpawned -= OnCubeSpawned;
    }

    private IEnumerator FillPool(float time)
    {
        WaitForSeconds wait = new WaitForSeconds(time);

        while (_cubeSpawner.PoolInactive <= _cubeSpawner.PoolCapacity)
        {
            yield return wait;
            _cubeSpawner.Spawn();
        }
    }

    private void OnCubeSpawned(Cube cube)
    {
        cube.Died += OnCubeDied;
    }

    private void OnCubeDied(Cube cube)
    {
        _bombSpawner.Spawn(cube.transform.position);
        cube.Died -= OnCubeDied;
    }
}
