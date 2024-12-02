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
        _textCubeActive.text = "���������� �������� �������� �� �����: " + _cubeSpawner.PoolActive;
        _textBombActive.text = "���������� �������� �������� �� �����: " + _bombSpawner.PoolActive;
    }

    private void OnCubeSpawned(Cube cube)
    {
        _countCubesSpawn++;
        _textSpawnCubes.text = "���������� ����������� �������� �� �� �����: " + _countCubesSpawn;
    }

    private void OnBombSpawned()
    {
        _countBombsSpawn++;
        _textSpawnBombs.text = "���������� ����������� �������� �� �� �����: " + _countBombsSpawn;
    }

    private void OnCubeCreated()
    {
        _cubeCreated++;
        _textCubeCreated.text = "���������� ��������� ��������: " + _cubeCreated;
    }

    private void OnBombCreated()
    {
        _bombCreated++;
        _textBombCreated.text = "���������� ��������� ��������: " + _bombCreated;
    }
}
