using System;
using TMPro;
using UnityEngine;

public class SpawnerCounter : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private TMP_Text _textSpawnCubes;
    [SerializeField] private TMP_Text _textSpawnBombs;
    [SerializeField] private TMP_Text _textCubeCreated;
    [SerializeField] private TMP_Text _textBombCreated;
    [SerializeField] private TMP_Text _textCubeActive;
    [SerializeField] private TMP_Text _textBombActive;

    private int _countCubesSpawn;
    private int _countBombsSpawn;
    private int _cubeCreated;
    private int _bombCreated;

    private void OnEnable()
    {
        _cubeSpawner.CubeSpawned += OnCubeSpawned;
        _bombSpawner.BombSpawned += OnBombSpawned;
        _cubeSpawner.ObjectCreated += OnCubeCreated;
        _bombSpawner.ObjectCreated += OnBombCreated;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeSpawned -= OnCubeSpawned;
        _bombSpawner.BombSpawned -= OnBombSpawned;
        _cubeSpawner.ObjectCreated -= OnCubeCreated;
        _bombSpawner.ObjectCreated -= OnBombCreated;
    }

    private void Update()
    {
        _textCubeActive.text = "Количество активных объектов на сцене: " + _cubeSpawner.PoolActive;
        _textBombActive.text = "Количество активных объектов на сцене: " + _bombSpawner.PoolActive;
    }

    private void OnCubeSpawned(Cube cube)
    {
        _countCubesSpawn++;
        _textSpawnCubes.text = "Количество заспавненых объектов за всё время: " + _countCubesSpawn;
    }

    private void OnBombSpawned()
    {
        _countBombsSpawn++;
        _textSpawnBombs.text = "Количество заспавненых объектов за всё время: " + _countBombsSpawn;
    }

    private void OnCubeCreated()
    {
        _cubeCreated++;
        _textCubeCreated.text = "Количество созданных объектов: " + _cubeCreated;
    }

    private void OnBombCreated()
    {
        _bombCreated++;
        _textBombCreated.text = "Количество созданных объектов: " + _bombCreated;
    }
}
